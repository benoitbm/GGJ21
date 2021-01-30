//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.10
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


using System;
using System.Runtime.InteropServices;

namespace Noesis
{

public class DoubleAnimation : BaseAnimation {
  internal new static DoubleAnimation CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new DoubleAnimation(cPtr, cMemoryOwn);
  }

  internal DoubleAnimation(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(DoubleAnimation obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public static DependencyProperty ByProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.DoubleAnimation_ByProperty_get();
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty FromProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.DoubleAnimation_FromProperty_get();
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty ToProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.DoubleAnimation_ToProperty_get();
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public Nullable<float> From {
    set {
      NullableFloat tempvalue = value;
      NoesisGUI_PINVOKE.DoubleAnimation_From_set(swigCPtr, ref tempvalue);
    }

    get {
      IntPtr ret = NoesisGUI_PINVOKE.DoubleAnimation_From_get(swigCPtr);
      if (ret != IntPtr.Zero) {
        return Marshal.PtrToStructure<NullableFloat>(ret);
      }
      else {
        return new Nullable<float>();
      }
    }

  }

  public Nullable<float> To {
    set {
      NullableFloat tempvalue = value;
      NoesisGUI_PINVOKE.DoubleAnimation_To_set(swigCPtr, ref tempvalue);
    }

    get {
      IntPtr ret = NoesisGUI_PINVOKE.DoubleAnimation_To_get(swigCPtr);
      if (ret != IntPtr.Zero) {
        return Marshal.PtrToStructure<NullableFloat>(ret);
      }
      else {
        return new Nullable<float>();
      }
    }

  }

  public Nullable<float> By {
    set {
      NullableFloat tempvalue = value;
      NoesisGUI_PINVOKE.DoubleAnimation_By_set(swigCPtr, ref tempvalue);
    }

    get {
      IntPtr ret = NoesisGUI_PINVOKE.DoubleAnimation_By_get(swigCPtr);
      if (ret != IntPtr.Zero) {
        return Marshal.PtrToStructure<NullableFloat>(ret);
      }
      else {
        return new Nullable<float>();
      }
    }

  }

  public DoubleAnimation() {
  }

  protected override IntPtr CreateCPtr(Type type, out bool registerExtend) {
    registerExtend = false;
    return NoesisGUI_PINVOKE.new_DoubleAnimation();
  }

}

}

