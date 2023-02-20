using Quoter.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Services.FormAnimation
{
    public interface IFormAnimationService
    {
        Task AnimateAsync(Form form, EnumAnimation enumAnimation);

        Task CloseDelayedAsync(Form form, int delay, EnumAnimation enumAnimation = EnumAnimation.FadeOut);
    }
}
