using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.ViewModels
{
    public class PantryViewModel : BaseViewModel
    {
        public int SelectedTabIndex { get; set; }

        public PantryViewModel()
        {
            SelectedTabIndex = 0;
        }

    }
}
