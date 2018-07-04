// Copyright (c) Russlan Akiev. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace IdentityBase.GoogleRecaptcha
{
    using System.Threading.Tasks;
    using IdentityBase.Forms;

    public class GoogleRecaptchaCreateViewModelAction :
       ILoginCreateViewModelAction,
       IRecoverCreateViewModelAction
    {
        private readonly GoogleRecaptchaOptions _recaptchaOptions;
        
        public GoogleRecaptchaCreateViewModelAction(
            GoogleRecaptchaOptions recaptchaOptions)
        {
            this._recaptchaOptions = recaptchaOptions;
        }

        public int Step => 0;

        public async Task Execute(CreateViewModelContext context)
        {
            //ModelClass model = new ModelClass();
            //context.Items
            //    .Add(nameof(LoginCreateViewModelAction), model);

            context.FormElements.Add(new FormElement
            {
                Name = "recapcha",
                Before = "submit",
                ViewName = "FormElements/Recapcha",
                Model = new GoogleRecaptchaViewModel
                {
                    SiteKey = this._recaptchaOptions.SiteKey
                }
            });
        }
    }
}