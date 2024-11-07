using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CynVee_Hash_Checker;

internal class CustomHasher
{
    private readonly Dictionary<Algorithm, HashAlgorithm> _hashAlgorithms = new Dictionary<Algorithm, HashAlgorithm>();

    private bool _complete = false;

    internal CustomHasher()
    {
        _hashAlgorithms.Add(Algorithm.SHA1, SHA1.Create());
        _hashAlgorithms.Add(Algorithm.SHA256, SHA256.Create());
        _hashAlgorithms.Add(Algorithm.SHA512, SHA512.Create());
        _hashAlgorithms.Add(Algorithm.SHA384, SHA384.Create());
        _hashAlgorithms.Add(Algorithm.MD5, MD5.Create());

        foreach (var algorithm in _hashAlgorithms)
        {
            algorithm.Value.Initialize();
        }
    }

    internal void TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount)
    {
        if (_complete) { throw new InvalidOperationException("Cannot transform block after final transform."); }

        foreach (var algorithm in _hashAlgorithms)
        {
            algorithm.Value.TransformBlock(inputBuffer, inputOffset, inputCount, null, 0);
        }
    }

    internal void TransformFinalBlock()
    {
        foreach (var algorithm in _hashAlgorithms)
        {
            algorithm.Value.TransformFinalBlock(Array. Empty<byte>(), 0, 0);
        }

        _complete = true;
    }

    internal Dictionary<Algorithm, string> GetHashes()
    {
        if (!_complete) { throw new InvalidOperationException("Cannot get hashes until final block transform"); }

        var returnDictionary = new Dictionary<Algorithm, string>();
        foreach (var algorithm in _hashAlgorithms)
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