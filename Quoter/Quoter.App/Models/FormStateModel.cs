using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Models
{
    public class FormStateModel
    {
        public Type Type { get; set; }

        public Form Form { get; set; }

        public bool IsModal { get; set; }

        public FormStateModel(Type type, Form form, bool isModal)
        {
            Type = type;
            Form = form;
            IsModal = isModal;
        }
    }
}
