﻿using CodexBank.Common.Utils;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CodexApi.Web.Infrastructure.Helpers.PaymentHelpers
{
    public static class DirectPaymentsHelper
    {
        public static string DecodePaymentRequest(string encoded)
            => Encoding.UTF8.GetString(Convert.FromBase64String(encoded));

        public static dynamic ParsePaymentRequest(string jsonRequest)
        {
            dynamic request = JsonConvert.DeserializeObject(jsonRequest);

            if (!ValidateSignature(request))
            {
                return null;
            }

            var paymentInfo = GetPaymentInfo(request);
            if (paymentInfo.Amount <= 0)
            {
                return null;
            }

            return request;
        }

        public static string GeneratePaymentRequestWithProof(dynamic request, string bankPublicKey, string codexApiKey)
        {
            string paymentInfoJson = request.PaymentInfo;
            string websitePaymentInfoSignature = request.PaymentInfoSignature;
            string returnUrl = request.ReturnUrl;

            // generate PaymentProof containing the bank's public key
            // and merchant's original PaymentInfo signature
            var paymentProof = new
            {
                BankPublicKey = bankPublicKey,
                PaymentInfoSignature = websitePaymentInfoSignature
            };

            string paymentProofJson = JsonConvert.SerializeObject(paymentProof);


            string paymentInfoCodexApiSignature;
            string paymentProofSignature;

            // sign the PaymentInfo and PaymentProof
            using (var codexApiRsa = RSA.Create())
            {
                RsaExtensions.FromXmlString(codexApiRsa, codexApiKey);

                paymentInfoCodexApiSignature = Convert.ToBase64String(
                    codexApiRsa.SignData(
                        Encoding.UTF8.GetBytes(paymentInfoJson),
                        HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));

                paymentProofSignature = Convert.ToBase64String(
                    codexApiRsa.SignData(
                        Encoding.UTF8.GetBytes(paymentProofJson),
                        HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            }

            var proofRequest = new
            {
                PaymentInfo = paymentInfoJson,
                PaymentInfoSignature = paymentInfoCodexApiSignature,
                PaymentProof = paymentProofJson,
                PaymentProofSignature = paymentProofSignature,
                ReturnUrl = returnUrl
            };

            string proofRequestJson = JsonConvert.SerializeObject(proofRequest);

            string encodedProofRequest = Convert.ToBase64String(Encoding.UTF8.GetBytes(proofRequestJson));

            return encodedProofRequest;
        }

        public static dynamic GetPaymentInfo(dynamic paymentRequest)
            => JsonConvert.DeserializeObject((string)paymentRequest.PaymentInfo);

        private static bool ValidateSignature(dynamic data)
        {
            string paymentInfoJson = data.PaymentInfo;
            string paymentInfoSignature = data.PaymentInfoSignature;
            string websitePublicKey = data.PublicKey;

            // validate PaymentInfo signature to make sure it has not been modified
            // (or at least make it more difficult to modify as it would require signing it with a new key)

            // ! This signature must also be verified by the merchant website after a successful payment
            using (var websiteRsa = RSA.Create())
            {
                RsaExtensions.FromXmlString(websiteRsa, websitePublicKey);

                bool isWebsiteSignatureValid = websiteRsa.VerifyData(
                    Encoding.UTF8.GetBytes(paymentInfoJson),
                    Convert.FromBase64String(paymentInfoSignature),
                    HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                return isWebsiteSignatureValid;
            }
        }
    }
}
