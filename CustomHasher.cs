using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CynVee_Hash_Checker;

internal class CustomHasher
{
    Dictionary<Algorithm, HashAlgorithm> hashAlgorithms = new Dictionary<Algorithm, HashAlgorithm>();

    bool complete = false;

    internal CustomHasher()
    {
        hashAlgorithms.Add(Algorithm.SHA1, SHA1.Create());
        hashAlgorithms.Add(Algorithm.SHA256, SHA256.Create());
        hashAlgorithms.Add(Algorithm.SHA512, SHA512.Create());
        hashAlgorithms.Add(Algorithm.SHA384, SHA384.Create());
        hashAlgorithms.Add(Algorithm.MD5, MD5.Create());

        foreach (var algorithm in hashAlgorithms)
        {
            algorithm.Value.Initialize();
        }
    }

    internal void TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount)
    {
        if (complete) { throw new InvalidOperationException("Cannot transform block after final transform."); }

        foreach (var algorithm in hashAlgorithms)
        {
            algorithm.Value.TransformBlock(inputBuffer, inputOffset, inputCount, null, 0);
        }
    }

    internal void TransformFinalBlock()
    {
        foreach (var algorithm in hashAlgorithms)
        {
            algorithm.Value.TransformFinalBlock(new byte[0], 0, 0);
        }

        complete = true;
    }

    internal Dictionary<Algorithm, string> GetHashes()
    {
        if (!complete) { throw new InvalidOperationException("Cannot get hashes until final block transform"); }

        var returnDictionary = new Dictionary<Algorithm, string>();
        foreach (var algorithm in hashAlgorithms)
        {
            
            returnDictionary.Add(algorithm.Key, BitConverter.ToString(algorithm.Value.Hash!).Replace("-", String.Empty).ToLowerInvariant());
        }

        return returnDictionary;
    }

}

internal enum Algorithm
{
    MD5,
    SHA1,
    SHA256,
    SHA384,
    SHA512
}