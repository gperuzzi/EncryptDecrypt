using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncriptCSVFiles
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                foreach (string s in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csv").Select(System.IO.Path.GetFileName))
                {

                    string FileName = String.Concat(Directory.GetCurrentDirectory(), "\\", s);
                    string[] a = FileName.Split('.');
                    string FileNameTmp = a[0] + "tmp" + a[1];
                    string password = @"myKey123"; // Your Key Here
                    UnicodeEncoding UE = new UnicodeEncoding();
                    byte[] key = UE.GetBytes(password);

                    string cryptFile = FileNameTmp;
                    FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                    RijndaelManaged RMCrypto = new RijndaelManaged();

                    CryptoStream cs = new CryptoStream(fsCrypt,
                        RMCrypto.CreateEncryptor(key, key),
                        CryptoStreamMode.Write);

                    FileStream fsIn = new FileStream(FileName, FileMode.Open);

                    int data;
                    while ((data = fsIn.ReadByte()) != -1)
                        cs.WriteByte((byte)data);


                    fsIn.Close();
                    cs.Close();
                    fsCrypt.Close();
                    File.Delete(FileName);
                    File.Move(FileNameTmp, FileName);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
                Console.ReadLine();
            }
            Console.WriteLine("Success");
            Console.ReadLine();
        }
    }
}
