﻿//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Microsoft.Xna.Framework.Graphics;

namespace LetsCreateZelda.Components
{
    class Script : Component
    {

        private ScriptEngine _engine;
        private string _path;
        private dynamic _scope;
        private object _scriptClass; 

        public override ComponentType ComponentType
        {
            get { return ComponentType.Script;  }
        }

        public Script(string path)
        {
            _engine = Python.CreateEngine();
            _engine.Runtime.LoadAssembly(Assembly.GetExecutingAssembly());
            _path = Path.Combine("Content/Scripts/", path + ".py");
            _scope = _engine.CreateScope();
            _scope.owner = this;
            _engine.ExecuteFile(_path, _scope);
            _scriptClass = _engine.Operations.CreateInstance(_scope.GetVariable("Script"));
            _engine.Operations.InvokeMember(_scriptClass, "initialize"); 
        }

        public override void Update(double gameTime)
        {
            _engine.Operations.InvokeMember(_scriptClass, "run", gameTime);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
           
        }
    }
}




