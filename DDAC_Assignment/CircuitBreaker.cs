using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDAC_Assignment
{
    //Base class for circuitState
    public abstract class CircuitBreakerState
    {
        protected readonly CircuitBreaker circuitBreaker;

        //Constructor receives a instance of a Circuit Breaker class
        protected CircuitBreakerState(CircuitBreaker circuitBreaker)
        {
            this.circuitBreaker = circuitBreaker;
        }

        //Execute the protected code
        public virtual CircuitBreaker ProtectedCodeIsAboutToBeCalled()
        {
            return this.circuitBreaker;
        }

        //Protected code has been called (is completed, either successfully or not)
        public virtual void ProtectedCodeHasBeenCalled() { }

        //When protected code raises an exception, increment the failure count on the circuitBreaker
        public virtual void ActUponException(Exception e) { circuitBreaker.IncreaseFailureCount(); }

        //return the current state class instance
        public virtual CircuitBreakerState Update()
        {
            return this;
        }
    }

    //Represents an Open (not functional) state
    public class OpenState : CircuitBreakerState
    {
        private readonly DateTime openDateTime;

        //Set the openDateTime to the current UTC time
        public OpenState(CircuitBreaker circuitBreaker) : base(circuitBreaker)
        {
            openDateTime = DateTime.UtcNow;
        }

        //Call the protected code
        public override CircuitBreaker ProtectedCodeIsAboutToBeCalled()
        {
            base.ProtectedCodeIsAboutToBeCalled();
            this.Update();
            return base.circuitBreaker;
        }

        //if timeout period is passed, move the state to HalfOpen
        public override CircuitBreakerState Update()
        {
            base.Update();
            if (DateTime.UtcNow >= openDateTime + base.circuitBreaker.Timeout)
            {
                return circuitBreaker.MoveToHalfOpenState();
            }
            return this;
        }
    }

    //Represent halfopen state
    //i.e. We are ready to try again after a timeout period
    public class HalfOpenState : CircuitBreakerState
    {
        //Call the base class constructor
        public HalfOpenState(CircuitBreaker circuitBreaker) : base(circuitBreaker) { }

        //if an exception is thrown by the protected code, Move to the open state
        public override void ActUponException(Exception e)
        {
            base.ActUponException(e);
            circuitBreaker.MoveToOpenState();
        }

        //if no exception was thrown, move to the Closed state
        public override void ProtectedCodeHasBeenCalled()
        {
            base.ProtectedCodeHasBeenCalled();
            circuitBreaker.MoveToClosedState();
        }
    }

    //Represent a close (functional) state
    public class ClosedState: CircuitBreakerState
    {
        //Call the Base Class Constructor and reset the failure count to 0
        public ClosedState(CircuitBreaker circuitBreaker):base(circuitBreaker)
        {
            circuitBreaker.ResetFailureCount();
        }

        //if an exception is raised by the protected code,
        //and the failure threshold has been reached, move to the Open state
        public override void ActUponException(Exception e)
        {
            base.ActUponException(e);
            if (circuitBreaker.IsThresholdReached())
            {
                circuitBreaker.MoveToOpenState();
            }
        }
    }

    //main CircuitBreaker class
    public class CircuitBreaker
    {
        //Used to Lock code blocks
        private readonly object monitor = new object();

        //Used to track the state of the circuit
        private CircuitBreakerState state;

        //Number of failures
        public int Failures { get; private set; }

        //Threshold count that causes the circuit to open
        public int Threshold { get; private set; }

        //How long to wait before trying to reset the circuit
        public TimeSpan Timeout { get; private set; }

        //Constructor
        public CircuitBreaker(int threshold, TimeSpan timeout)
        {
            if (threshold < 1)
            {
                throw new ArgumentOutOfRangeException("threshold", "Threshold should be greater than 0");
            }

            if (timeout.TotalMilliseconds < 1)
            {
                throw new ArgumentOutOfRangeException("timeout", "Timeout should be greater than 0");
            }

            Threshold = threshold;
            Timeout = timeout;
            MoveToClosedState();
        }

        //ReadOnly properties representing the state of circuit
        public bool isClosed
        {
            get { return state.Update() is ClosedState; }
        }

        public bool isOpen
        {
            get { return state.Update() is OpenState; }
        }

        public bool isHalfOpen
        {
            get { return state.Update() is HalfOpenState; }
        }

        //Move the circuit to closed state
        internal CircuitBreakerState MoveToClosedState()
        {
            state = new ClosedState(this);
            return state;
        }

        //Move the circuit to open state
        internal CircuitBreakerState MoveToOpenState()
        {
            state = new OpenState(this);
            return state;
        }

        //Move the circuit to half open state
        internal CircuitBreakerState MoveToHalfOpenState()
        {
            state = new HalfOpenState(this);
            return state;
        }

        //increment the failure count
        internal void IncreaseFailureCount()
        {
            Failures++;
        }

        //reset the failure count
        internal void ResetFailureCount()
        {
            Failures = 0;
        }

        //has the failure threshold been reached?
        public bool IsThresholdReached()
        {
            return Failures >= Threshold;
        }

        //references var used to hold a ref to the exception from the last call
        private Exception exceptionFromLastAttemptCall = null;

        //get the exception (if any) from last attempted call
        public Exception GetExceptionFromLastAttemptCall()
        {
            return exceptionFromLastAttemptCall;
        }

        //try to run the code
        public CircuitBreaker AttemptCall(Action protectedCode)
        {
            this.exceptionFromLastAttemptCall = null;
            lock (monitor)
            {
                state.ProtectedCodeIsAboutToBeCalled();
                if (state is OpenState)
                {
                    return this; // stop execution of this method
                }
            }

            try
            {
                protectedCode();
            }
            catch (Exception e)
            {
                this.exceptionFromLastAttemptCall = e;
                lock (monitor)
                {
                    state.ActUponException(e);
                }
                return this; // stop execution of this method
            }

            lock (monitor)
            {
                state.ProtectedCodeHasBeenCalled();
            }
            return this;
        }

        //close the circuit
        public void Close()
        {
            lock (monitor)
            {
                MoveToClosedState();
            }
        }

        //open the circuit
        public void Open()
        {
            lock (monitor)
            {
                MoveToOpenState();
            }
        }
    }
}