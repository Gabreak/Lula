#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEditor;
using Sirenix.OdinInspector;
using UnityEngine.Localization;

[assembly: RegisterValidationRule(typeof(MyValueValidator), Name = "MyValueValidator", Description = "Some description text.")]

[Required]
public class MyValueValidator : ValueValidator<LocalizeStringEvent>
{
    // Introduce serialized fields here to make your validator
    // configurable from the validator window under rules.
    public int SerializedConfig;

    protected override void Validate(ValidationResult result)
    {


        //Value.StringReference.GetLocalizedString()
        {
            //result.AddWarning(Value.ToString());
        }
        //var val = this.Value;

        //if (val has something wrong with it)
        //{
        //    result.AddError("Something is wrong");
        //
    }
}
#endif
