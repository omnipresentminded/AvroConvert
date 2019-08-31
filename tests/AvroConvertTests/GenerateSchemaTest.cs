namespace AvroConvertTests
{
    using AvroConvert;
    using Xunit;

    public class GenerateSchemaTest
    {

        [Fact]
        public void GenerateSchema_PropertiesAreDecoratedWithDefaultValueAttributes_SchemaContainsDefaultFieldForMembers()
        {
            //Arrange

            //Act
            string schema = AvroConvert.GenerateSchema(typeof(DefaultValueClass));

            //Assert
            Assert.Contains("\"default\":\"Let's go\"", schema);
            Assert.Contains("\"default\":9200000000000000007", schema);
            Assert.Contains("\"default\":null", schema);
        }


        [Fact]
        public void GenerateSchema_PropertiesAreDecoratedWithDefaultValueAttributes_SchemaPositionsNullTypeBeforeOtherInTheTypeArrayWhenDefaultIsNull()
        {
            //Arrange

            //Act
            string schema = AvroConvert.GenerateSchema(typeof(DefaultValueClass));

            //Assert
            Assert.Contains("{\"name\":\"andNullProperty\",\"type\":[\"null\",\"long\"],\"default\":null}", schema);
        }

        [Fact]
        public void GenerateSchema_PropertiesIncludeNullableVersionsOfTypes_SchemaIncludesNullTypeInTypesArray()
        {
            //Arrange

            //Act
            string schema = AvroConvert.GenerateSchema(typeof(DefaultValueClass));

            //Assert
            Assert.Contains("{\"name\":\"andLongProperty\",\"type\":[\"null\",\"long\"]", schema);
        }

        [Fact]
        public void GenerateSchema_PropertiesAreDecoratedWithDefaultValueAttributes_SchemaPositionsNullTypeAfterOtherInTheTypeArrayWhenDefaultIsNotNull()
        {
            //Arrange

            //Act
            string schema = AvroConvert.GenerateSchema(typeof(DefaultValueClass));

            //Assert
            Assert.Contains("{\"name\":\"andLongBigDefaultedProperty\",\"type\":[\"long\",\"null\"],\"default\":9200000000000000007}", schema);
        }

        [Fact]
        public void GenerateSchema_PropertiesAreDecoratedWithDefaultValueAttributes_SchemaPositionsTypesEffectivelyRegardlessOfMismatchBetweenDefaultValueAndPropertyType()
        {
            //Arrange

            //Act
            string schema = AvroConvert.GenerateSchema(typeof(DefaultValueClass));

            // Assert - The DefaultValue is an int, (100)  but the property is a long, matching 
            // isn't necessary, all that's required is that the 'not null' type is first in the schema list
            Assert.Contains("{\"name\":\"andLongSmallDefaultedProperty\",\"type\":[\"long\",\"null\"],\"default\":100}", schema);
        }

        [Fact]
        public void GenerateSchema_PropertiesAreDecoratedWithDefaultValueAttributes_SchemaPositionsOriginalTypeBeforeNullWhenDefaultIsNotNull()
        {
            //Arrange

            //Act
            string schema = AvroConvert.GenerateSchema(typeof(DefaultValueClass));

            //Assert
            Assert.Contains("{\"name\":\"justSomeProperty\",\"type\":[\"string\",\"null\"],\"default\":\"Let's go\"}", schema);
        }

        [Fact]
        public void GenerateSchema_ClassWithMixedMembersAttributesAndNon_AfterIncludingOnlyMembersNonAttributedAreIgnored()
        {
            //Arrange

            //Act
            string schema = AvroConvert.GenerateSchema(typeof(MixedDataMembers), includeOnlyDataContractMembers: true);

            //Assert
            Assert.Contains("{\"type\":\"record\",\"name\":\"AvroConvertTests.MixedDataMembers\",\"fields\":[{\"name\":\"savedValues\",\"type\":{\"type\":\"array\",\"items\":\"int\"}},{\"name\":\"andAnother\",\"type\":[\"null\",\"long\"]}]}", schema);
        }

        [Fact]
        public void GenerateSchema_ClassWithSimpleListWithoutDataMemberAttribute_SchemaContainsListAsArray(){
            //Arrange

            //Act
            string schema = AvroConvert.GenerateSchema(typeof(ClassWithSimpleList));
            //Asert
            Assert.Contains("[{\"name\":\"someList\",\"type\":{\"type\":\"array\",\"items\":\"int\"}}]", schema);
        }

        [Fact]
        public void GenerateSchema_ClassWithSimpleListWithDatatMemberAttribute_SchemaContainsListAsArray(){
            //Arrange

            //Act
            string schema = AvroConvert.GenerateSchema(typeof(ClassWithSimpleListAndDataMemberAttribute), includeOnlyDataContractMembers: true);
            //Asert
            Assert.Contains("[{\"name\":\"someList\",\"type\":{\"type\":\"array\",\"items\":\"int\"}}]", schema);
        }

        
        [Fact]
        public void GenerateSchema_ComplexClassWithoutDataMemberAttribute_SchemaContainsTheComplexObjects(){
            //Arrange

            //Act
            string schema = AvroConvert.GenerateSchema(typeof(VeryComplexClass));
            //Asert
            Assert.Contains("[{\"name\":\"ClassesWithArray\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"name\":\"AvroConvertTests.ClassWithArray\",\"fields\":[{\"name\":\"theArray\",\"type\":{\"type\":\"array\",\"items\":\"int\"}}]}}},{\"name\":\"ClassesWithGuid\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"name\":\"AvroConvertTests.ClassWithGuid\",\"fields\":[{\"name\":\"theGuid\",\"type\":{\"type\":\"fixed\",\"name\":\"System.Guid\",\"size\":16}}]}}},{\"name\":\"anotherClass\",\"type\":{\"type\":\"record\",\"name\":\"AvroConvertTests.ClassWithConstructorPopulatingProperty\",\"fields\":[{\"name\":\"nestedList\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"name\":\"AvroConvertTests.NestedTestClass\",\"fields\":[{\"name\":\"justSomeProperty\",\"type\":[\"null\",\"string\"]},{\"name\":\"andLongProperty\",\"type\":\"long\"}]}}},{\"name\":\"anotherList\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"name\":\"AvroConvertTests.ClassWithSimpleList\",\"fields\":[{\"name\":\"someList\",\"type\":{\"type\":\"array\",\"items\":\"int\"}}]}}},{\"name\":\"stringProperty\",\"type\":[\"null\",\"string\"]}]}},{\"name\":\"simpleClass\",\"type\":{\"type\":\"record\",\"name\":\"AvroConvertTests.User\",\"fields\":[{\"name\":\"name\",\"type\":[\"null\",\"string\"]},{\"name\":\"favorite_number\",\"type\":[\"null\",\"int\"]},{\"name\":\"favorite_color\",\"type\":[\"null\",\"string\"]}]}},{\"name\":\"simpleObject\",\"type\":\"int\"},{\"name\":\"bools\",\"type\":{\"type\":\"array\",\"items\":\"boolean\"}},{\"name\":\"doubleProperty\",\"type\":\"double\"},{\"name\":\"floatProperty\",\"type\":\"float\"}]", schema);
            Assert.Contains("[{\"name\":\"ClassesWithArray\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"name\":\"AvroConvertTests.ClassWithArray\",\"fields\":[{\"name\":\"theArray\",\"type\":{\"type\":\"array\",\"items\":\"int\"}}]}}}", schema);
            Assert.Contains("{\"name\":\"ClassesWithGuid\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"name\":\"AvroConvertTests.ClassWithGuid\",\"fields\":[{\"name\":\"theGuid\",\"type\":{\"type\":\"fixed\",\"name\":\"System.Guid\",\"size\":16}}]}}}", schema);
            Assert.Contains("{\"name\":\"anotherClass\",\"type\":{\"type\":\"record\",\"name\":\"AvroConvertTests.ClassWithConstructorPopulatingProperty\",\"fields\":[{\"name\":\"nestedList\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"name\":\"AvroConvertTests.NestedTestClass\",\"fields\":[{\"name\":\"justSomeProperty\",\"type\":[\"null\",\"string\"]},{\"name\":\"andLongProperty\",\"type\":\"long\"}]}}},{\"name\":\"anotherList\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"name\":\"AvroConvertTests.ClassWithSimpleList\",\"fields\":[{\"name\":\"someList\",\"type\":{\"type\":\"array\",\"items\":\"int\"}}]}}},{\"name\":\"stringProperty\",\"type\":[\"null\",\"string\"]}]}},{\"name\":\"simpleClass\",\"type\":{\"type\":\"record\",\"name\":\"AvroConvertTests.User\",\"fields\":[{\"name\":\"name\",\"type\":[\"null\",\"string\"]},{\"name\":\"favorite_number\",\"type\":[\"null\",\"int\"]},{\"name\":\"favorite_color\",\"type\":[\"null\",\"string\"]}]}}", schema);
            Assert.Contains("{\"name\":\"simpleClass\",\"type\":{\"type\":\"record\",\"name\":\"AvroConvertTests.User\",\"fields\":[{\"name\":\"name\",\"type\":[\"null\",\"string\"]},{\"name\":\"favorite_number\",\"type\":[\"null\",\"int\"]},{\"name\":\"favorite_color\",\"type\":[\"null\",\"string\"]}]}}", schema);
            Assert.Contains("{\"name\":\"simpleObject\",\"type\":\"int\"}", schema);
            Assert.Contains("{\"name\":\"doubleProperty\",\"type\":\"double\"}", schema);
            Assert.Contains("{\"name\":\"floatProperty\",\"type\":\"float\"}]", schema);

        }

        [Fact]
        public void GenerateSchema_ComplexClassWithDatatMemberAttribute_SchemaContainsTheComplexObjects(){
            //Arrange

            //Act
            string schema = AvroConvert.GenerateSchema(typeof(VeryComplexClassWithDataMemberAttribute), includeOnlyDataContractMembers: true);
            //Asert
            Assert.Contains("[{\"name\":\"ClassesWithArray\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"name\":\"AvroConvertTests.ClassWithArray\",\"fields\":[{\"name\":\"theArray\",\"type\":{\"type\":\"array\",\"items\":\"int\"}}]}}}", schema);
            Assert.Contains("{\"name\":\"ClassesWithGuid\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"name\":\"AvroConvertTests.ClassWithGuid\",\"fields\":[{\"name\":\"theGuid\",\"type\":{\"type\":\"fixed\",\"name\":\"System.Guid\",\"size\":16}}]}}}", schema);
            Assert.Contains("{\"name\":\"anotherClass\",\"type\":{\"type\":\"record\",\"name\":\"AvroConvertTests.ClassWithConstructorPopulatingProperty\",\"fields\":[{\"name\":\"nestedList\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"name\":\"AvroConvertTests.NestedTestClass\",\"fields\":[{\"name\":\"justSomeProperty\",\"type\":[\"null\",\"string\"]},{\"name\":\"andLongProperty\",\"type\":\"long\"}]}}},{\"name\":\"anotherList\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"name\":\"AvroConvertTests.ClassWithSimpleList\",\"fields\":[{\"name\":\"someList\",\"type\":{\"type\":\"array\",\"items\":\"int\"}}]}}},{\"name\":\"stringProperty\",\"type\":[\"null\",\"string\"]}]}},{\"name\":\"simpleClass\",\"type\":{\"type\":\"record\",\"name\":\"AvroConvertTests.User\",\"fields\":[{\"name\":\"name\",\"type\":[\"null\",\"string\"]},{\"name\":\"favorite_number\",\"type\":[\"null\",\"int\"]},{\"name\":\"favorite_color\",\"type\":[\"null\",\"string\"]}]}}", schema);
            Assert.Contains("{\"name\":\"simpleClass\",\"type\":{\"type\":\"record\",\"name\":\"AvroConvertTests.User\",\"fields\":[{\"name\":\"name\",\"type\":[\"null\",\"string\"]},{\"name\":\"favorite_number\",\"type\":[\"null\",\"int\"]},{\"name\":\"favorite_color\",\"type\":[\"null\",\"string\"]}]}}", schema);
            Assert.Contains("{\"name\":\"simpleObject\",\"type\":\"int\"}", schema);
            Assert.Contains("{\"name\":\"doubleProperty\",\"type\":\"double\"}", schema);
            Assert.Contains("{\"name\":\"floatProperty\",\"type\":\"float\"}]", schema);

        }

    }
}