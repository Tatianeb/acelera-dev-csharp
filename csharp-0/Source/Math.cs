using System;
using System.Collections.Generic;

namespace Codenation.Challenge
{
    public class Math
    {
        public  List<int>Fibonacci()
        {
            var numeros = new List<Int32>();
            int numeroAnterior = 0;
            int numeroAtual = 1;
            int fibonacci = 0;

            numeros.Add(0);

            fibonacci = numeroAnterior + numeroAtual;
            while (fibonacci < 378)
            {
                numeros.Add(fibonacci);
                fibonacci = numeroAnterior + numeroAtual;

                numeroAnterior = numeroAtual;
                numeroAtual = fibonacci;
            }
            return numeros;
        }
        public bool IsFibonacci(int numberToTest)
        {
            var numeros = new List<Int32>();
            int numeroAnterior = 0;
            int numeroAtual = 1;
            int fibonacci = 0;

            numeros.Add(0);

            fibonacci = numeroAnterior + numeroAtual;
            while (fibonacci < 378)
            {
                numeros.Add(fibonacci);
                fibonacci = numeroAnterior + numeroAtual;

                numeroAnterior = numeroAtual;
                numeroAtual = fibonacci;
            }
            return numeros.Contains(numberToTest);
            //qqthrow new NotImplementedException();
        }
    }
}
