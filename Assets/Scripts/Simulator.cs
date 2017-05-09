using NLua;
using UnityEngine;
using System;

public class Simulator {

  private CarAPI carapi;
  private Lua luaenv;
  private string script;

  public Simulator(string script) {
    luaenv = new Lua ();
    luaenv.LoadCLRPackage ();
    luaenv.DoString (@"
        import = function () end
    ");
    luaenv.DoString (script);
  }

  public void start(GameState state) {
    carapi = new CarAPI (state);
    luaenv ["car"] = carapi;
    CallScript ("Start");
  }

  public CarAction update(GameState state) {
    CallScript ("Update");
    CarAction action = carapi.getActionAndReset ();
    if(state.enteredNewSegment) {
      CallScript ("NewSection");
      state.enteredNewSegment = false;
    }
    return action;
  }

  public System.Object[] CallScript(string function) {
    LuaFunction lf = luaenv.GetFunction (function);
    if(lf == null)
      return null;
    else
      return lf.Call ();
  }
}
