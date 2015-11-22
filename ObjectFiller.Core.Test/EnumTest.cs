﻿using System;
    using Xunit;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{

    public class EnumTest
    {
        [Fact]
        public void Must_support_enums_out_of_the_box()
        {
            var filler = new Filler<MyClass>();
            filler.Setup()
                .OnProperty(x => x.Manual).Use(() => ManualSetupEnum.B)
                .OnProperty(x => x.Ignored).IgnoreIt();

            for (int n = 0; n < 1000; n++)
            {
                var c = filler.Create();

                Assert.True(
                    c.Standard == StandardEnum.A ||
                    c.Standard == StandardEnum.B ||
                    c.Standard == StandardEnum.C);

                Assert.True(
                    c.Numbered == NumberedEnum.A ||
                    c.Numbered == NumberedEnum.B ||
                    c.Numbered == NumberedEnum.C);

                Assert.True(
                    c.Flags == FlagsEnum.A ||
                    c.Flags == FlagsEnum.B ||
                    c.Flags == FlagsEnum.C);

                Assert.True((int)c.Nasty == 0);

                Assert.True(c.Manual == ManualSetupEnum.B);

                Assert.True((int)c.Ignored == 0);
            }
        }

        [Fact]
        public void Must_support_class_with_enums_as_ctor_out_of_the_box()
        {
            var filler = new Filler<MyClassWithCstr>();
            filler.Setup().OnProperty(x => x.Manual).Use(() => ManualSetupEnum.B);

            for (int n = 0; n < 1000; n++)
            {
                var c = filler.Create();

                Assert.True(
                    c.Standard == StandardEnum.A ||
                    c.Standard == StandardEnum.B ||
                    c.Standard == StandardEnum.C);

                Assert.True(
                    c.Numbered == NumberedEnum.A ||
                    c.Numbered == NumberedEnum.B ||
                    c.Numbered == NumberedEnum.C);

                Assert.True(
                    c.Flags == FlagsEnum.A ||
                    c.Flags == FlagsEnum.B ||
                    c.Flags == FlagsEnum.C);

                Assert.True((int)c.Nasty == 0);

                Assert.True(c.Manual == ManualSetupEnum.B);
            }
        }

        [Fact]
        public void FillNullableEnum()
        {
            var filler = new Filler<ClassWithNullableEnum>();

            var c = filler.Create();
            Assert.True(
                c.NullableEnum == StandardEnum.A ||
                c.NullableEnum == StandardEnum.B ||
                c.NullableEnum == StandardEnum.C);
        }


        public enum StandardEnum
        {
            A, B, C
        }

        public enum NumberedEnum
        {
            A = 1, B = 3, C = 5
        }

        [Flags]
        public enum FlagsEnum
        {
            A = 0x01,
            B = 0x02,
            C = A | B,
        }

        [Flags]
        public enum NastyEmptyEnum
        {
        }

        public enum ManualSetupEnum
        {
            A, B, C
        }

        public enum IgnoredEnum
        {
            A, B, C
        }

        public class MyClass
        {
            public StandardEnum Standard { get; set; }
            public NumberedEnum Numbered { get; set; }
            public FlagsEnum Flags { get; set; }
            public NastyEmptyEnum Nasty { get; set; }
            public ManualSetupEnum Manual { get; set; }
            public IgnoredEnum Ignored { get; set; }
        }

        public class MyClassWithCstr
        {
            public MyClassWithCstr(StandardEnum standard, NumberedEnum numbered, FlagsEnum flags, ManualSetupEnum manual, IgnoredEnum ignored)
            {
                this.Standard = standard;
                this.Numbered = numbered;
                this.Flags = flags;
                this.Manual = manual;
                this.Ignored = ignored;
            }

            public StandardEnum Standard { get; set; }
            public NumberedEnum Numbered { get; set; }
            public FlagsEnum Flags { get; set; }
            public NastyEmptyEnum Nasty { get; set; }
            public ManualSetupEnum Manual { get; set; }
            public IgnoredEnum Ignored { get; set; }
        }

        public class ClassWithNullableEnum
        {
            public StandardEnum? NullableEnum { get; set; }
        }
    }
}
