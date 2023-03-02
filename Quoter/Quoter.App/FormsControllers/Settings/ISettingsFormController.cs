﻿using Quoter.App.Forms;
using Quoter.Framework.Enums;

namespace Quoter.App.FormsControllers.Settings
{
    public interface ISettingsFormController : IFormController<ISettingsForm>
    {

        string NotificationsIntervalMinutes { get; set; }

        string NotificationsAutoCloseSeconds { get; set; }

		string OpacityValue { get; set; }

        int AutoCloseNotificationsSeconds { get; set; }

        void SetTheme(EnumTheme theme);

        void SetLanguage(EnumLanguage language);

        void SetShowCollectionsBasedOnLanguage(bool value);

        void SetShowWelcomeMessage(bool value);

        void SetOpacity(double opacity);

        void SetNotificationType(EnumNotificationType type);
    }
}
