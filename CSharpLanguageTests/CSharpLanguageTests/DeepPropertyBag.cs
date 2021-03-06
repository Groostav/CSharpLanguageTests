﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoMapper;

namespace CSharpLanguageTests
{
    public interface IVersionablePropertyBag
    {
    }

    [DebuggerDisplay("DeepPropertyBag : StringProperty = {StringProperty}")]
    public class DeepPropertyBag : ICloneable, IEquatable<DeepPropertyBag>, IVersionablePropertyBag
    {
        static DeepPropertyBag()
        {
            //TODO remove this
            Mapper.CreateMap<DeepPropertyBag, DeepPropertyBag>();
        }

        public virtual DeepPropertyBag DeepChild { get; set; }
        public virtual FlatPropertyBag FlatChild { get; set; }

        public virtual IList<FlatPropertyBag> ChildBags { get; set; }
        public virtual string DeepStringProperty { get; set; }

        public object Clone()
        {
            var returnable = Mapper.Map<DeepPropertyBag>(this);
            return returnable;
        }

        public bool Equals(DeepPropertyBag other)
        {
            throw new NotImplementedException();
        }
    }

    public class GrandDeepPropertyBag
    {
        public virtual DeepPropertyBag DeepChild { get; set; }
    } 

    [DebuggerDisplay("FlatPropertyBag : StringProperty = {StringProperty}")]
    public class FlatPropertyBag : ICloneable, IEquatable<FlatPropertyBag>, IVersionablePropertyBag
    {
        public virtual string StringProperty { get; set; }
        public virtual int IntProperty { get; set; }
        public virtual string PropWithoutSetter { get { return StringProperty; } }

        public object Clone()
        {
            return Mapper.Map<FlatPropertyBag>(this);
        }

        #region Equality Nonsense

        public bool Equals(FlatPropertyBag other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(StringProperty, other.StringProperty) && IntProperty == other.IntProperty;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FlatPropertyBag)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (StringProperty != null ? StringProperty.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IntProperty;
                return hashCode;
            }
        }

        public static bool operator ==(FlatPropertyBag left, FlatPropertyBag right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FlatPropertyBag left, FlatPropertyBag right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}