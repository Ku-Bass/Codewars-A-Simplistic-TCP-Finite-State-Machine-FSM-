public class TCP
{
    public static string TraverseStates(string[] events)
    {
        State currentState = State.CLOSED; // initial state, always
                                           // Your code here!
        foreach (string signal in events)
        {
            currentState = GetNextState(currentState, signal);
        }
        return currentState.ToString();
    }
    public static State GetNextState(State currentState, string signal)
    {
        return (currentState, signal) switch
        {

            (State.CLOSED, "APP_PASSIVE_OPEN") => State.LISTEN,
            (State.CLOSED, "APP_ACTIVE_OPEN") => State.SYN_SENT,
            (State.LISTEN, "RCV_SYN") => State.SYN_RCVD,
            (State.LISTEN, "APP_SEND") => State.SYN_SENT,
            (State.LISTEN, "APP_CLOSE") => State.CLOSED,
            (State.SYN_RCVD, "APP_CLOSE") => State.FIN_WAIT_1,
            (State.SYN_RCVD, "RCV_ACK") => State.ESTABLISHED,
            (State.SYN_SENT, "RCV_SYN") => State.SYN_RCVD,
            (State.SYN_SENT, "RCV_SYN_ACK") => State.ESTABLISHED,
            (State.SYN_SENT, "APP_CLOSE") => State.CLOSED,
            (State.ESTABLISHED, "APP_CLOSE") => State.FIN_WAIT_1,
            (State.ESTABLISHED, "RCV_FIN") => State.CLOSE_WAIT,
            (State.FIN_WAIT_1, "RCV_FIN") => State.CLOSING,
            (State.FIN_WAIT_1, "RCV_FIN_ACK") => State.TIME_WAIT,
            (State.FIN_WAIT_1, "RCV_ACK") => State.FIN_WAIT_2,
            (State.CLOSING, "RCV_ACK") => State.TIME_WAIT,
            (State.FIN_WAIT_2, "RCV_FIN") => State.TIME_WAIT,
            (State.TIME_WAIT, "APP_TIMEOUT") => State.CLOSED,
            (State.CLOSE_WAIT, "APP_CLOSE") => State.LAST_ACK,
            (State.LAST_ACK, "RCV_ACK") => State.CLOSED,
            _ => State.ERROR

        };

    }
    //    CLOSED: APP_PASSIVE_OPEN -> LISTEN
    //CLOSED: APP_ACTIVE_OPEN  -> SYN_SENT
    //LISTEN: RCV_SYN          -> SYN_RCVD
    //LISTEN: APP_SEND         -> SYN_SENT
    //LISTEN: APP_CLOSE        -> CLOSED
    //SYN_RCVD: APP_CLOSE      -> FIN_WAIT_1
    //SYN_RCVD: RCV_ACK        -> ESTABLISHED
    //SYN_SENT: RCV_SYN        -> SYN_RCVD
    //SYN_SENT: RCV_SYN_ACK    -> ESTABLISHED
    //SYN_SENT: APP_CLOSE      -> CLOSED
    //ESTABLISHED: APP_CLOSE   -> FIN_WAIT_1
    //ESTABLISHED: RCV_FIN     -> CLOSE_WAIT
    //FIN_WAIT_1: RCV_FIN      -> CLOSING
    //FIN_WAIT_1: RCV_FIN_ACK  -> TIME_WAIT
    //FIN_WAIT_1: RCV_ACK      -> FIN_WAIT_2
    //CLOSING: RCV_ACK         -> TIME_WAIT
    //FIN_WAIT_2: RCV_FIN      -> TIME_WAIT
    //TIME_WAIT: APP_TIMEOUT   -> CLOSED
    //CLOSE_WAIT: APP_CLOSE    -> LAST_ACK
    //LAST_ACK: RCV_ACK        -> CLOSED
    public enum State
    {
        CLOSED,
        LISTEN,
        SYN_SENT,
        SYN_RCVD, ESTABLISHED,
        CLOSE_WAIT,
        LAST_ACK,
        FIN_WAIT_1,
        FIN_WAIT_2,
        CLOSING,
        TIME_WAIT,
        ERROR
    }
 
}