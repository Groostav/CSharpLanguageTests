using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using AutoMapper.Impl;
using CSharpLanguageTests;

namespace VersionCommander.Implementation
{
    [DebuggerDisplay("TimestampedPropertyVersionDelta: Set {TargetSite.Name} to {_arguments[0]}")]
    public class TimestampedPropertyVersionDelta : TimestampedVersionDelta
    {
        public TimestampedPropertyVersionDelta(object setValue, MethodInfo targetSite, long timeStamp, bool isActive = true)
            : base(new[] {setValue}, targetSite, timeStamp, isActive)
        {

        }

        public TimestampedPropertyVersionDelta(TimestampedPropertyVersionDelta delta, object newSetValue)
            : base(delta, new[] {newSetValue})
        {
        }

        public TimestampedPropertyVersionDelta(TimestampedPropertyVersionDelta delta)
            : base(delta)
        {
        }

        public bool IsSettingVersioningObject
        {
            get { throw new NotImplementedException(); }
        }
    }

    [DebuggerDisplay("TimestampedVersionDelta: Invoke {TargetSite.Name} with {Arguments}")]
    public class TimestampedVersionDelta
    {
        private readonly List<object> _arguments;

        public TimestampedVersionDelta(TimestampedVersionDelta delta)
            : this(delta.Arguments, delta.TargetSite, delta.TimeStamp, delta.IsActive)
        {
        }

        public TimestampedVersionDelta(TimestampedVersionDelta delta, IEnumerable<object> newArguments)
            : this(newArguments, delta.TargetSite, delta.TimeStamp, delta.IsActive)
        {
        }

        public TimestampedVersionDelta(IEnumerable<object> arguments, MethodInfo targetSite, long timeStamp, bool isActive = true)
        {
            if (arguments == null) throw new ArgumentNullException("arguments");
            if (targetSite == null) throw new ArgumentNullException("targetSite");

            _arguments = arguments.ToList();
            TimeStamp = timeStamp;
            TargetSite = targetSite;
            IsActive = isActive;
        }

        public MethodInfo TargetSite { get; private set; }

        public IEnumerable<object> Arguments
        {
            get { return _arguments.AsReadOnly(); }
        }

        public long TimeStamp { get; private set; }

        public bool IsActive { get; set; } //dont like this, otherwise immutable object made mutable.
        //infact its responsible for a bug. 
        //decorator?

        public object InvokedOn(object target)
        {
            return TargetSite.Invoke(target, Arguments.ToArray());
        }

        public TimestampedVersionDelta Clone()
        {
            return new TimestampedVersionDelta(this);
        }
    }
}

namespace VersionCommander.Implementation.Visitors
{
    public interface IVersionControlTreeVisitor
    {
        void OnFirstEntry();
        void OnEntry(IVersionControlNode controlNode);
        void OnExit(IVersionControlNode controlNode);
        void OnLastExit();

        bool VisitAllNodes { get; }
    }

    public abstract class VersionControlTreeVisitorBase : IVersionControlTreeVisitor
    {
        public VersionControlTreeVisitorBase()
        {
            _enteredOnce = false;
            _exitedOnce = false;
        }

        private bool _enteredOnce;
        private bool _exitedOnce;

        public virtual void OnFirstEntry()
        {
            if (_enteredOnce) throw new Exception();
            _enteredOnce = true;
        }

        public virtual void OnEntry(IVersionControlNode controlNode)
        {
        }

        public virtual void OnExit(IVersionControlNode controlNode)
        {
        }

        public virtual void OnLastExit()
        {
            if (_exitedOnce) throw new Exception();
            _exitedOnce = true;
        }

        public virtual bool VisitAllNodes
        {
            get { return true; }
        }
    }
}

namespace VersionCommander.Implementation
{
    public interface IProxyFactory
    {
        TSubject CreateVersioning<TSubject>(ICloneFactory<TSubject> cloneFactory,
                                            IVersionControlNode existingControlNode = null,
                                            TSubject existingObject = null)
            where TSubject : class;
    }
    public interface ICloneFactory<TCloned>
    {
        TCloned CreateCloneOf(TCloned target);
        TCloned CreateNew();
    }
}
