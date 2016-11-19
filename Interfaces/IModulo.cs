using EM3.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace EM3.Interfaces
{
    interface IModulo
    {
        UserControl GetContentControl();

        void Inject(Principal principal);
    }
}
