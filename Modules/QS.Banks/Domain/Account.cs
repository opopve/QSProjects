﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using QS.DomainModel.Entity;
using QS.DomainModel.Entity.EntityPermissions;

namespace QS.Banks.Domain
{
	[Appellative (Gender = GrammaticalGender.Masculine,
		Nominative = "расчётный счет",
		NominativePlural = "расчётные счета"
	)]
	[EntityPermission]
	public class Account : PropertyChangedBase, IValidatableObject, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		string name;

		public virtual string Name {
			get {
				if(string.IsNullOrEmpty(name)) {
					return "Основной";
				}
				return name; 
			}
			set { SetField (ref name, value, () => Name); }
		}

		string number;

		[StringLength (25, MinimumLength = 20, ErrorMessage = "Номер банковского счета должен содержать 20 цифр и не превышать 25-ти.")]
		[Display (Name = "Номер")]
		public virtual string Number {
			get { return number; }
			set { SetField (ref number, value, () => Number); }
		}

		public virtual string Title{
			get{
				return "Счет: " + Name;
			}
		}
		string code1c;

		[Display (Name = "Код 1с")]
		public virtual string Code1c {
			get { return code1c; }
			set { SetField (ref code1c, value, () => Code1c); }
		}

		bool inactive;

		[Display (Name = "Неактивный")]
		public virtual bool Inactive {
			get { return inactive; }
			set { SetField (ref inactive, value, () => Inactive); }
		}

		Bank inBank;

		[Required (ErrorMessage = "Банк должен быть заполнен.")]
		[Display (Name = "Банк")]
		public virtual Bank InBank {
			get { return inBank; }
			set {SetField (ref inBank, value, () => InBank);
				Inactive = InBank == null || InBank.Deleted;
			}
		}

		CorAccount bankCorAccount;
		[Display(Name = "Кор. счет выбранного банка")]
		public virtual CorAccount BankCorAccount {
			get { return bankCorAccount; }
			set { SetField(ref bankCorAccount, value, () => BankCorAccount); }
		}

		#endregion

		bool isDefault;

		public virtual bool IsDefault {
			get { return isDefault; }
			set {
				if(SetField(ref isDefault, value, () => IsDefault) && value && Owner != null){
					foreach(var item in Owner.Accounts) {
						if(item != this) {
							item.IsDefault = false;
						}
					}
				}
			}
		}

		/// <summary>
		/// Ссылка на владельца счета. Необходима для возможности установки счета по умолчанию.
		/// </summary>
		public virtual IAccountOwner Owner { get; set; }

		public Account ()
		{
		}
		#region IValidatableObject implementation

		public virtual IEnumerable<ValidationResult> Validate (ValidationContext validationContext)
		{
			if(!new Regex (@"^[0-9]*$").IsMatch (Number))
				yield return new ValidationResult("Номер счета может содержать только цифры.", new[]{ "Number" });
			if(BankCorAccount == null || !BankCorAccount.InBank.Equals(InBank)) {
				yield return new ValidationResult("Должен быть выбран кор. счет выбранного банка.");
			}
		}

		#endregion
	}
}

