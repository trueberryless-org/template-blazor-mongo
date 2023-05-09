namespace Model.ConnectionConfig.Exceptions; 

public class MissingConnectionStringException : Exception{
    public MissingConnectionStringException() {
        
    }
    public MissingConnectionStringException(string msg) : base(msg) {

    }
}