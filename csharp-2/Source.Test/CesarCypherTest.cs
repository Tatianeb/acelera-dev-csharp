using System;
using Xunit;

namespace Codenation.Challenge
{
    public class CesarCypherTest
    {
        [Fact]
        public void Should_Not_Accept_Null_When_Crypt()
        {            
            var cypher = new CesarCypher();
            Assert.Throws<ArgumentNullException>(() => cypher.Crypt(null));
        }

        [Fact]
        public void Should_Keep_Numbers_Out_When_Crypt()
        {
            var cypher = new CesarCypher();
            Assert.Equal("d", cypher.Crypt("a"));
        }

    }
}
