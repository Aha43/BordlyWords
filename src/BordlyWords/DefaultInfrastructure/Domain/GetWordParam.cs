﻿using BordlyWords.Specification.Domain.Param;
using System.Globalization;

namespace BordlyWords.DefaultInfrastructure.Domain
{
    public class GetWordParam : IGetWordParam
    {
        public CultureInfo Culture { get; init; } = new CultureInfo("nb-NO");

        private int? _length = null;
        public int? Length
        {
            get { return _length; } 
            init { _length = value.EnsureLegalWordLength(); }
        }
    }
}
