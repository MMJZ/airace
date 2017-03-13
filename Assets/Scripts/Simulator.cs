using NLua;

public class Simulator {

  private CarAPI carapi;
  private TrackAPI trackapi;
  private Lua luaenv;
  private string script;

  public Simulator() {
    carapi = new CarAPI ();
    trackapi = new TrackAPI ();
    luaenv = new Lua ();
    luaenv.LoadCLRPackage ();
    luaenv ["car"] = carapi;
    luaenv ["track"] = trackapi;
  }

  public void loadScript(string script) {
    luaenv.DoString (script);
  }

  public void start(GameState state) {
    carapi.Update (state);
    trackapi.Update (state);
    CallScript ("Start");
  }

  public CarAction update(GameState state) {
    carapi.Update (state);
    trackapi.Update (state);
    CallScript ("Update");
    CarAction action = carapi.getActionAndReset ();
    if(state.enteredNewSegment)
      CallScript ("NewSection");
    return action;
  }

  public int getParentScriptID() {
    System.Object[] res = CallScript ("ParentScript");
    if(res == null)
      return 0;
    else
			//TODO convert function result to int safely; return 0 if error
			return 0;
  }

  public System.Object[] CallScript(string function) {
    LuaFunction lf = luaenv.GetFunction (function);
    if(lf == null)
      return null;
    else
      return lf.Call ();
  }
}
