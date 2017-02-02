using Base.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Base.Interfaces
{
    interface IModulo
    {
        UserControl GetContentControl();

        void Inject(Principal principal);
    }
}
