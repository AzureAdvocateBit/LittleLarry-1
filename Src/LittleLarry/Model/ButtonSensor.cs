﻿using GHIElectronics.UWP.Shields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleLarry.Model
{
    public class ButtonSensor
    {
        private FEZHAT _hat;
        private Queue<Mode> _buttons;
        private Mode _mode;
        public ButtonSensor(FEZHAT hat)
        {
            _hat = hat;
            // tracking "long" pushes
            _buttons = new Queue<Mode>(10);
            _mode = Mode.Idle;
        }

        public void Process()
        {
            // handle button pushes
            if (_hat.IsDIO18Pressed() && _hat.IsDIO22Pressed()) _buttons.Enqueue(Mode.Auto);
            else if (_hat.IsDIO18Pressed()) _buttons.Enqueue(Mode.Learn);
            else if (_hat.IsDIO22Pressed()) _buttons.Enqueue(Mode.Model);
            else _buttons.Enqueue(Mode.Idle);

            if (_buttons.Count > 10) _buttons.Dequeue();

            if (_buttons.Count > 9 && _buttons.All(m => m == Mode.Auto))
            {
                _mode = _mode == Mode.Auto ? Mode.Idle : Mode.Auto;
                _buttons.Clear();
            }
            else if (_buttons.Count > 9 && _buttons.All(m => m == Mode.Learn))
            {
                _mode = _mode == Mode.Learn ? Mode.Idle : Mode.Learn;
                _buttons.Clear();
            }
            else if (_buttons.Count > 9 && _buttons.All(m => m == Mode.Model))
            {
                _mode = _mode == Mode.Model ? Mode.Idle : Mode.Model;
                _buttons.Clear();
            }
        }

        public Mode Mode
        {
            get { return _mode; }
        }
    }
}
