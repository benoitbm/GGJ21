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

public partial class TextureSource : ImageSource {
  internal new static TextureSource CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new TextureSource(cPtr, cMemoryOwn);
  }

  internal TextureSource(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(TextureSource obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public TextureSource() {
  }

  protected override IntPtr CreateCPtr(Type type, out bool registerExtend) {
    registerExtend = false;
    return NoesisGUI_PINVOKE.new_TextureSource__SWIG_0();
  }

  public TextureSource(Texture texture) : this(NoesisGUI_PINVOKE.new_TextureSource__SWIG_1(Texture.getCPtr(texture)), true) {
  }

  public int PixelWidth {
    get {
      int ret = NoesisGUI_PINVOKE.TextureSource_PixelWidth_get(swigCPtr);
      return ret;
    } 
  }

  public int PixelHeight {
    get {
      int ret = NoesisGUI_PINVOKE.TextureSource_PixelHeight_get(swigCPtr);
      return ret;
    } 
  }

  public Texture Texture {
    set {
      NoesisGUI_PINVOKE.TextureSource_Texture_set(swigCPtr, Texture.getCPtr(value));
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.TextureSource_Texture_get(swigCPtr);
      Texture ret = (cPtr == IntPtr.Zero) ? null : new Texture(cPtr, false);
      return ret;
    } 
  }

}

}
