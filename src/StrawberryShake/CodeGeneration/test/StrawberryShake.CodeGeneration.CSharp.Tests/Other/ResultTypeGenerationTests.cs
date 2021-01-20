using System.Text;
using System.Threading.Tasks;
using Snapshooter.Xunit;
using StrawberryShake.CodeGeneration;
using StrawberryShake.CodeGeneration.CSharp;
using Xunit;

namespace StrawberryShake.Other
{
    public class ResultTypeGenerationTests
    {
        readonly StringBuilder _stringBuilder;
        readonly CodeWriter _codeWriter;
        readonly ResultTypeGenerator _generator;

        public ResultTypeGenerationTests()
        {
            _stringBuilder = new StringBuilder();
            _codeWriter = new CodeWriter(_stringBuilder);
            _generator = new ResultTypeGenerator();
        }

        [Fact]
        public async Task GenerateSimpleClassWithOneValueTypeProperty()
        {
            await _generator.WriteAsync(
                _codeWriter,
                new TypeDescriptor(
                    "Foo",
                    "FooBarNamespace",
                    new string[] { },
                    new[] {TestHelper.GetNamedNonNullStringTypeReference("SomeText"),}
                )
            );
            _stringBuilder.ToString().MatchSnapshot();
        }

        [Fact]
        public async Task GenerateSimpleClassWithOneReferenceTypeProperty()
        {
            await _generator.WriteAsync(
                _codeWriter,
                new TypeDescriptor(
                    "Foo",
                    "FooBarNamespace",
                    new string[] { },
                    new[]
                    {
                        new NamedTypeReferenceDescriptor(
                            new TypeDescriptor(
                                "Bar",
                                "BarNamespace",
                                isEntityType: true
                            ),
                            false,
                            ListType.NoList,
                            "Bar"
                        )
                    }
                )
            );
            _stringBuilder.ToString().MatchSnapshot();
        }

        [Fact]
        public async Task GenerateSimpleClassWithOneNullableValueTypeProperty()
        {
            await _generator.WriteAsync(
                _codeWriter,
                new TypeDescriptor(
                    "Foo",
                    "FooBarNamespace",
                    new string[] { },
                    new[] {TestHelper.GetNamedNonNullStringTypeReference("SomeText"),}
                )
            );
            _stringBuilder.ToString().MatchSnapshot();
        }

        [Fact]
        public async Task GenerateSimpleClassWithOneNullableReferenceTypeProperty()
        {
            await _generator.WriteAsync(
                _codeWriter,
                new TypeDescriptor(
                    "Foo",
                    "FooBarNamespace",
                    new string[] { },
                    new[]
                    {
                        new NamedTypeReferenceDescriptor(
                            new TypeDescriptor(
                                "Bar",
                                "BarNamespace",
                                isEntityType: true
                            ),
                            true,
                            ListType.NoList,
                            "Bar"
                        )
                    }
                )
            );
            _stringBuilder.ToString().MatchSnapshot();
        }

        [Fact]
        public async Task GenerateSimpleClassWithImplements()
        {
            await _generator.WriteAsync(
                _codeWriter,
                new TypeDescriptor(
                    "Foo",
                    "FooBarNamespace",
                    new string[] {"IFoo", "IBar"},
                    new[]
                    {
                        new NamedTypeReferenceDescriptor(
                            new TypeDescriptor(
                                "Bar",
                                "BarNamespace",
                                isEntityType: true
                            ),
                            false,
                            ListType.NoList,
                            "Bar"
                        )
                    }
                )
            );
            _stringBuilder.ToString().MatchSnapshot();
        }


        [Fact]
        public async Task GenerateSimpleClassWithMultipleProperties()
        {
            await _generator.WriteAsync(
                _codeWriter,
                new TypeDescriptor(
                    "Foo",
                    "FooBarNamespace",
                    new string[] { },
                    new[]
                    {
                        TestHelper.GetNamedNonNullStringTypeReference("Id"), new NamedTypeReferenceDescriptor(
                            new TypeDescriptor(
                                "Bar",
                                "BarNamespace",
                                isEntityType: true
                            ),
                            false,
                            ListType.List,
                            "Bars"
                        ),
                        new NamedTypeReferenceDescriptor(
                            new TypeDescriptor(
                                "Baz",
                                "BazNamespace",
                                isEntityType: true
                            ),
                            true,
                            ListType.NoList,
                            "Baz"
                        ),
                    }
                )
            );
            _stringBuilder.ToString().MatchSnapshot();
        }
    }
}