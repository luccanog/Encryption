class Assymetric : IEncryptor
{
    protected Assymetric()
    {

    }
    static Assymetric()
    {
        _assymetric = new Assymetric();
    }
    private static Assymetric _assymetric;
    public static Assymetric Instance
    {
        get
        {
            return _assymetric;
        }
    }
    public void Encrypt()
    {

    }

    public void Decrypt()
    {

    }
}