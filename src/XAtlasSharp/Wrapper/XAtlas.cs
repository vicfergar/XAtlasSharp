//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace XAtlasSharp {

public class XAtlas {
  public static int GetChartSize() {
    int ret = XAtlasPINVOKE.GetChartSize();
    return ret;
  }

  public static int GetVertexSize() {
    int ret = XAtlasPINVOKE.GetVertexSize();
    return ret;
  }

  public static int GetMeshSize() {
    int ret = XAtlasPINVOKE.GetMeshSize();
    return ret;
  }


  public delegate bool ProgressFuncDelegate(ProgressCategory category, int progress, System.IntPtr userData);
  public delegate System.IntPtr ReallocFuncDelegate(System.IntPtr ptr, ulong size);
  public delegate void FreeFuncDelegate(System.IntPtr ptr);
  public delegate int PrintFuncDelegate(string message);

  public static uint KImageChartIndexMask {
    get {
      uint ret = XAtlasPINVOKE.KImageChartIndexMask_get();
      return ret;
    } 
  }

  public static uint KImageHasChartIndexBit {
    get {
      uint ret = XAtlasPINVOKE.KImageHasChartIndexBit_get();
      return ret;
    } 
  }

  public static uint KImageIsBilinearBit {
    get {
      uint ret = XAtlasPINVOKE.KImageIsBilinearBit_get();
      return ret;
    } 
  }

  public static uint KImageIsPaddingBit {
    get {
      uint ret = XAtlasPINVOKE.KImageIsPaddingBit_get();
      return ret;
    } 
  }

  internal static Atlas Create() {
    global::System.IntPtr cPtr = XAtlasPINVOKE.Create();
    Atlas ret = (cPtr == global::System.IntPtr.Zero) ? null : new Atlas(cPtr, false);
    return ret;
  }

  internal static void Destroy(Atlas Atlas) {
    XAtlasPINVOKE.Destroy(Atlas.getCPtr(Atlas));
  }

  internal static AddMeshError AddMesh(Atlas Atlas, MeshDecl MeshDecl, uint MeshCountHint) {
    AddMeshError ret = (AddMeshError)XAtlasPINVOKE.AddMesh__SWIG_0(Atlas.getCPtr(Atlas), MeshDecl.getCPtr(MeshDecl), MeshCountHint);
    if (XAtlasPINVOKE.SWIGPendingException.Pending) throw XAtlasPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  internal static AddMeshError AddMesh(Atlas Atlas, MeshDecl MeshDecl) {
    AddMeshError ret = (AddMeshError)XAtlasPINVOKE.AddMesh__SWIG_1(Atlas.getCPtr(Atlas), MeshDecl.getCPtr(MeshDecl));
    if (XAtlasPINVOKE.SWIGPendingException.Pending) throw XAtlasPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  internal static void AddMeshJoin(Atlas Atlas) {
    XAtlasPINVOKE.AddMeshJoin(Atlas.getCPtr(Atlas));
  }

  internal static AddMeshError AddUvMesh(Atlas Atlas, UvMeshDecl Decl) {
    AddMeshError ret = (AddMeshError)XAtlasPINVOKE.AddUvMesh(Atlas.getCPtr(Atlas), UvMeshDecl.getCPtr(Decl));
    if (XAtlasPINVOKE.SWIGPendingException.Pending) throw XAtlasPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  internal static void ComputeCharts(Atlas Atlas, ChartOptions Options) {
    XAtlasPINVOKE.ComputeCharts__SWIG_0(Atlas.getCPtr(Atlas), ChartOptions.getCPtr(Options));
    if (XAtlasPINVOKE.SWIGPendingException.Pending) throw XAtlasPINVOKE.SWIGPendingException.Retrieve();
  }

  internal static void ComputeCharts(Atlas Atlas) {
    XAtlasPINVOKE.ComputeCharts__SWIG_1(Atlas.getCPtr(Atlas));
  }

  internal static void PackCharts(Atlas Atlas, PackOptions PackOptions) {
    XAtlasPINVOKE.PackCharts__SWIG_0(Atlas.getCPtr(Atlas), PackOptions.getCPtr(PackOptions));
    if (XAtlasPINVOKE.SWIGPendingException.Pending) throw XAtlasPINVOKE.SWIGPendingException.Retrieve();
  }

  internal static void PackCharts(Atlas Atlas) {
    XAtlasPINVOKE.PackCharts__SWIG_1(Atlas.getCPtr(Atlas));
  }

  internal static void Generate(Atlas Atlas, ChartOptions ChartOptions, PackOptions PackOptions) {
    XAtlasPINVOKE.Generate__SWIG_0(Atlas.getCPtr(Atlas), ChartOptions.getCPtr(ChartOptions), PackOptions.getCPtr(PackOptions));
    if (XAtlasPINVOKE.SWIGPendingException.Pending) throw XAtlasPINVOKE.SWIGPendingException.Retrieve();
  }

  internal static void Generate(Atlas Atlas, ChartOptions ChartOptions) {
    XAtlasPINVOKE.Generate__SWIG_1(Atlas.getCPtr(Atlas), ChartOptions.getCPtr(ChartOptions));
    if (XAtlasPINVOKE.SWIGPendingException.Pending) throw XAtlasPINVOKE.SWIGPendingException.Retrieve();
  }

  internal static void Generate(Atlas Atlas) {
    XAtlasPINVOKE.Generate__SWIG_2(Atlas.getCPtr(Atlas));
  }

  internal static void SetProgressCallback(Atlas Atlas, XAtlas.ProgressFuncDelegate ProgressFunc, System.IntPtr ProgressUserData) {
    XAtlasPINVOKE.SetProgressCallback__SWIG_0(Atlas.getCPtr(Atlas), ProgressFunc, new System.Runtime.InteropServices.HandleRef(null, ProgressUserData));
  }

  internal static void SetProgressCallback(Atlas Atlas, XAtlas.ProgressFuncDelegate ProgressFunc) {
    XAtlasPINVOKE.SetProgressCallback__SWIG_1(Atlas.getCPtr(Atlas), ProgressFunc);
  }

  internal static void SetProgressCallback(Atlas Atlas) {
    XAtlasPINVOKE.SetProgressCallback__SWIG_2(Atlas.getCPtr(Atlas));
  }

  public static void SetAlloc(XAtlas.ReallocFuncDelegate ReallocFunc, XAtlas.FreeFuncDelegate FreeFunc) {
    XAtlasPINVOKE.SetAlloc__SWIG_0(ReallocFunc, FreeFunc);
  }

  public static void SetAlloc(XAtlas.ReallocFuncDelegate ReallocFunc) {
    XAtlasPINVOKE.SetAlloc__SWIG_1(ReallocFunc);
  }

  public static void SetPrint(XAtlas.PrintFuncDelegate Print, bool Verbose) {
    XAtlasPINVOKE.SetPrint(Print, Verbose);
  }

  public static string StringForEnum(AddMeshError Error) {
    string ret = XAtlasPINVOKE.StringForEnum__SWIG_0((int)Error);
    return ret;
  }

  public static string StringForEnum(ProgressCategory Category) {
    string ret = XAtlasPINVOKE.StringForEnum__SWIG_1((int)Category);
    return ret;
  }

}

}
