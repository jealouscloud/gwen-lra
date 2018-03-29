﻿using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using Gwen;
using Gwen.Controls;
using System.Drawing;
using TestApplication.Tests;
namespace TestApplication
{
    public class Window : GameWindow
    {
        Gwen.Input.OpenTK input;
        Canvas Canvas;
        bool _slow = false;
        bool _steps = false;
        public Window() : base(600, 600, GraphicsMode.Default, "UI Test")
        {
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            input = new Gwen.Input.OpenTK(this);
            var renderer = new Gwen.Renderer.OpenTK();
            var skinpng = new Texture(renderer);
            var skinimg = new Bitmap(Image.FromFile("DefaultSkin.png"));
            var font = new Bitmap(Image.FromFile("gamefont_15_0.png"));
            var fontdata = System.IO.File.ReadAllText("gamefont_15.fnt");
            var colorxml = System.IO.File.ReadAllText("DefaultColors.xml");
            Gwen.Renderer.OpenTK.LoadTextureInternal(
                skinpng,
                skinimg);

            var fontpng = new Texture(renderer);
            Gwen.Renderer.OpenTK.LoadTextureInternal(
                fontpng,
                font);

            var gamefont_15 = new Gwen.Renderer.BitmapFont(
                renderer,
                fontdata,
                fontpng);

            var skin = new Gwen.Skin.TexturedBase(renderer,
                skinpng)
            { DefaultFont = gamefont_15 };
            // var skin = new Gwen.Skin.Simple(renderer) { DefaultFont = gamefont_15 };
            Canvas = new Canvas(skin);
            Canvas.SetSize(ClientSize.Width, ClientSize.Height);
            input.Initialize(Canvas);
            TestContainer container = new TestContainer(Canvas);
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Canvas.Skin.DefaultFont.Dispose();
            Canvas.Skin.Dispose();
            Canvas.Dispose();
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            if (_slow && !_steps)
                return;
            _steps = false;
            if (Canvas.Size != ClientSize)
            {
                Canvas.SetSize(ClientSize.Width, ClientSize.Height);
            }
            GL.ClearColor(255, 255, 255, 255);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Viewport(new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, ClientSize.Width, ClientSize.Height, 0, 0, 1);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            Canvas.RenderCanvas();
            Canvas.ShouldDrawBackground = true;
            SwapBuffers();
            base.OnRenderFrame(e);
        }
        protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            input.ProcessKeyDown(e);
            if (e.Key == OpenTK.Input.Key.F12)
            {
                _slow = !_slow;
            }
            if (e.Key == OpenTK.Input.Key.F11)
            {
                _steps = true;
            }
        }
        protected override void OnKeyUp(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);
            input.ProcessKeyUp(e);
        }
        protected override void OnMouseMove(OpenTK.Input.MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            input.ProcessMouseMessage(e);
        }
        protected override void OnMouseUp(OpenTK.Input.MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            input.ProcessMouseMessage(e);
        }
        protected override void OnMouseDown(OpenTK.Input.MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            input.ProcessMouseMessage(e);
        }
        protected override void OnMouseWheel(OpenTK.Input.MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            input.ProcessMouseMessage(e);
        }
    }
}
