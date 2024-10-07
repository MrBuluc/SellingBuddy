using PaymentService.Api.DTOs;
using System.Text.RegularExpressions;

namespace PaymentService.Api.Services
{
    public partial class CardService
    {
        [GeneratedRegex(@"^4[0-9]{12}(?:[0-9]{3})?$")]
        private static partial Regex Visa();

        [GeneratedRegex(@"^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$")]
        private static partial Regex MasterCard();

        [GeneratedRegex(@"^3[47][0-9]{13}$")]
        private static partial Regex AmericanExpress();

        [GeneratedRegex(@"^6(?:011|5[0-9]{2})[0-9]{12}$")]
        private static partial Regex Discover();

        public static ValidCardNumberResult IsValidCardNumber(CardDTO card)
        {
            if (card.Expiration < DateTime.Now) return new()
            {
                IsValid = false,
                ErrorMessage = "Card Expiration Date expired."
            };

            CardType? cardType = GetCardType(card.Number);

            if (cardType is null) return new()
            {
                IsValid = false,
                ErrorMessage = "Card Type is not supported. Supported Card Types: MasterCard, Visa, Discover and AmericanExpress."
            };

            int validDigits = 0;
            switch (cardType)
            {
                case CardType.MasterCard:
                case CardType.Visa:
                case CardType.Discover:
                    validDigits = 3;
                    break;
                case CardType.AmericanExpress:
                    validDigits = 4;
                    break;
            }
            return new()
            {
                IsValid = card.SecurityNumber.Length == validDigits && new Regex($"[0-9]{{{validDigits}}}").Match(card.SecurityNumber).Success,
                ErrorMessage = "Card Security Number not valid for type."
            };
        }

        private static CardType? GetCardType(string cardNumber)
        {
            if (Visa().Match(cardNumber).Success)
            {
                return CardType.Visa;
            }
            else if (MasterCard().Match(cardNumber).Success)
            {
                return CardType.MasterCard;
            }
            else if (AmericanExpress().Match(cardNumber).Success)
            {
                return CardType.AmericanExpress;
            }
            else if (Discover().Match(cardNumber).Success)
            {
                return CardType.Discover;
            }

            return null;
        }
    }

    enum CardType
    {
        Visa,
        MasterCard,
        AmericanExpress,
        Discover
    }

    public class ValidCardNumberResult
    {
        public bool IsValid { get; set; }
        public string? ErrorMessage { get; set; }

        public override string ToString() => $"IsValid: {IsValid}, ErrorMessage: {ErrorMessage}";
    }
}
