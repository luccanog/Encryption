
public class Encryptor
{
    IEncryptor _encryptor;

    public Encryptor(IEncryptor encryptor)
    {
        _encryptor = encryptor;
    }

    public void Run()
    {
        _encryptor.Encrypt();
    }
}