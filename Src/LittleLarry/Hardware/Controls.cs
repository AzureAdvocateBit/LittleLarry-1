﻿using GHIElectronics.UWP.Shields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;

namespace LittleLarry.Hardware
{
    public class Controls
    {
        private Gamepad _gamepad = null;
        public Controls(FEZHAT hat)
        {
            Gamepad.GamepadAdded += Gamepad_GamepadAdded;
            Gamepad.GamepadRemoved += Gamepad_GamepadRemoved;
            if (Gamepad.Gamepads.Count == 1)
                _gamepad = Gamepad.Gamepads[0];
        }




        private void Gamepad_GamepadRemoved(object sender, Gamepad e)
        {
            if (Gamepad.Gamepads.Count < 1)
                _gamepad = null;
            else
                _gamepad = Gamepad.Gamepads[0];
        }

        private void Gamepad_GamepadAdded(object sender, Gamepad e)
        {
            if (Gamepad.Gamepads.Count == 1)
                _gamepad = Gamepad.Gamepads[0];
        }

        public (double speed, double turn) GetValues()
        {
            if (_gamepad != null)
            {
                var state = _gamepad.GetCurrentReading();
                return (state.LeftThumbstickY, state.RightThumbstickX);
            }
            else return (0, 0);
        }

        public (double x, double y) GetRawValues()
        {
            if (_gamepad != null)
            {
                var state = _gamepad.GetCurrentReading();
                return (state.LeftThumbstickY, state.RightThumbstickX);
            }
            else return (0, 0);
        }

        public (double SpeedA, double SpeedB) Convert(double speed, double turn)
        {
            double GetFloor(double num)
            {
                double n = num;
                if (n < -1)
                    return -1;
                else if (n > 1)
                    return 1;
                else
                    return n;
            }

            double s = speed / 2d;
            double t = Math.Abs(turn / 2d);

            if (speed >= 0)
            {
                if (turn > 0)
                    return (GetFloor(s + t), GetFloor(s));
                else
                    return (GetFloor(s), GetFloor(s + t));
            }
            else
            {
                if (turn > 0)
                    return (GetFloor(s - t), GetFloor(s));
                else
                    return (GetFloor(s), GetFloor(s - t));
            }
        }
    }
}
