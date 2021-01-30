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
using System.IO;

namespace Noesis
{

public class FileTextureProvider : TextureProvider {
  internal new static FileTextureProvider CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new FileTextureProvider(cPtr, cMemoryOwn);
  }

  internal FileTextureProvider(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(FileTextureProvider obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  protected FileTextureProvider() {
  }

  public virtual Stream OpenStream(string filename) {
    return null;
  }

  internal new static IntPtr Extend(string typeName) {
    return NoesisGUI_PINVOKE.Extend_FileTextureProvider(Marshal.StringToHGlobalAnsi(typeName));
  }
}

}

