﻿/*
 * NetEbics -- .NET Core EBICS Client Library
 * (c) Copyright 2018 Bjoern Kuensting
 *
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.txt', which is part of this source code package.
 */

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using NetEbics.Config;

namespace NetEbics.Xml
{
    internal class XPathHelper
    {
        private XmlNamespaceManager _nm;
        private NamespaceConfig _nsc;
        private XDocument _doc;

        internal string Xml => _doc?.ToString();

        public XPathHelper(XDocument doc, NamespaceConfig nsc)
        {
            _doc = doc;
            _nsc = nsc;
            var r = _doc.CreateReader();
            _nm = new XmlNamespaceManager(r.NameTable);
            _nm.AddNamespace(_nsc.EbicsPrefix, _nsc.Ebics);
            _nm.AddNamespace(_nsc.XmlDsigPrefix, _nsc.XmlDsig);
        }

        private string DNS(string name)
        {
            return $"{_nsc.EbicsPrefix}:{name}";
        }

        private string SNS(string name)
        {
            return $"{_nsc.XmlDsigPrefix}:{name}";
        }

        internal XElement GetTechReturnCode()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.header)}/{DNS(XmlNames.mutable)}/{DNS(XmlNames.ReturnCode)}",
                _nm);
        }

        internal XElement GetBusReturnCode()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.body)}/{DNS(XmlNames.ReturnCode)}", _nm);
        }

        internal XElement GetOrderID()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.header)}/{DNS(XmlNames.staticHeader)}/{DNS(XmlNames.OrderID)}", _nm);
        }

        internal XElement GetTimestampBankParameter()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.body)}/{DNS(XmlNames.TimestampBankParameter)}", _nm);
        }

        internal XElement GetReportText()
        {
            //var s = $"{DNS(XMLNames.header)}/{DNS(XMLNames.mutable)}/{DNS(XMLNames.ReportText)}";
            var elem = _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.header)}/{DNS(XmlNames.mutable)}/{DNS(XmlNames.ReportText)}",
                _nm);
            return elem;
        }

        internal XElement GetOrderData()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.body)}/{DNS(XmlNames.DataTransfer)}/{DNS(XmlNames.OrderData)}", _nm);
        }

        internal XAttribute GetEncryptionPubKeyDigestVersion()
        {
            var elem = _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.body)}/{DNS(XmlNames.DataTransfer)}/{DNS(XmlNames.DataEncryptionInfo)}/{DNS(XmlNames.EncryptionPubKeyDigest)}",
                _nm);
            return elem?.Attribute(XmlNames.Version);
        }

        internal XElement GetEncryptionPubKeyDigest()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.body)}/{DNS(XmlNames.DataTransfer)}/{DNS(XmlNames.DataEncryptionInfo)}/{DNS(XmlNames.EncryptionPubKeyDigest)}",
                _nm);
        }

        internal XElement GetTransactionKey()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.body)}/{DNS(XmlNames.DataTransfer)}/{DNS(XmlNames.DataEncryptionInfo)}/{DNS(XmlNames.TransactionKey)}",
                _nm);
        }

        internal XElement GetAuthenticationPubKeyInfoX509Data()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.AuthenticationPubKeyInfo)}/{DNS(XmlNames.X509)}", _nm);
        }

        internal XElement GetEncryptionPubKeyInfoX509Data()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.EncryptionPubKeyInfo)}/{DNS(XmlNames.X509)}", _nm);
        }

        internal XElement GetEncryptionPubKeyInfoPubKeyValue()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.EncryptionPubKeyInfo)}/{DNS(XmlNames.PubKeyValue)}",
                _nm);
        }

        internal XElement GetAuthenticationPubKeyInfoPubKeyValue()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.AuthenticationPubKeyInfo)}/{DNS(XmlNames.PubKeyValue)}",
                _nm);
        }

        internal XElement GetAuthenticationPubKeyInfoAuthenticationVersion()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.AuthenticationPubKeyInfo)}/{DNS(XmlNames.AuthenticationVersion)}", _nm);
        }

        internal XElement GetEncryptionPubKeyInfoEncryptionVersion()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.EncryptionPubKeyInfo)}/{DNS(XmlNames.EncryptionVersion)}", _nm);
        }

        internal XElement GetAuthenticationPubKeyInfoModulus()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.AuthenticationPubKeyInfo)}/{DNS(XmlNames.PubKeyValue)}/{SNS(XmlNames.RSAKeyValue)}/{SNS(XmlNames.Modulus)}",
                _nm);
        }

        internal XElement GetAuthenticationPubKeyInfoExponent()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.AuthenticationPubKeyInfo)}/{DNS(XmlNames.PubKeyValue)}/{SNS(XmlNames.RSAKeyValue)}/{SNS(XmlNames.Exponent)}",
                _nm);
        }

        internal XElement GetEncryptionPubKeyInfoModulus()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.EncryptionPubKeyInfo)}/{DNS(XmlNames.PubKeyValue)}/{SNS(XmlNames.RSAKeyValue)}/{SNS(XmlNames.Modulus)}",
                _nm);
        }

        internal XElement GetEncryptionPubKeyInfoExponent()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.EncryptionPubKeyInfo)}/{DNS(XmlNames.PubKeyValue)}/{SNS(XmlNames.RSAKeyValue)}/{SNS(XmlNames.Exponent)}",
                _nm);
        }

        internal XElement GetTransactionID()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.header)}/{DNS(XmlNames.staticHeader)}/{DNS(XmlNames.TransactionID)}", _nm);
        }

        internal XElement GetNumSegments()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.header)}/{DNS(XmlNames.staticHeader)}/{DNS(XmlNames.NumSegments)}", _nm);
        }

        internal XElement GetTransactionPhase()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.header)}/{DNS(XmlNames.mutable)}/{DNS(XmlNames.TransactionPhase)}", _nm);
        }

        internal XElement GetSegmentNumber()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.header)}/{DNS(XmlNames.mutable)}/{DNS(XmlNames.SegmentNumber)}", _nm);
        }

        internal XElement GetAuthSignatureDigestValue()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.AuthSignature)}/{SNS(XmlNames.SignedInfo)}/{SNS(XmlNames.Reference)}/{SNS(XmlNames.DigestValue)}",
                _nm);
        }

        internal XElement GetAuthSignatureValue()
        {
            return _doc.XPathSelectElement(
                $"/*/{DNS(XmlNames.AuthSignature)}/{SNS(XmlNames.SignatureValue)}", _nm);
        }

        internal IEnumerable<XElement> GetAuthSignatureReferences()
        {
            return _doc.XPathSelectElements(
                $"/*/{DNS(XmlNames.AuthSignature)}/{SNS(XmlNames.SignedInfo)}/{SNS(XmlNames.Reference)}",
                _nm);
        }

        internal IEnumerable<XElement> GetAccessParamsUrls()
        {
            return _doc.XPathSelectElements($"/*/{DNS(XmlNames.AccessParams)}/{DNS(XmlNames.URL)}", _nm);
        }

        internal XElement GetAccessParamsInstitute()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.AccessParams)}/{DNS(XmlNames.Institute)}", _nm);
        }
        
        internal XElement GetAccessParamsHostId()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.AccessParams)}/{DNS(XmlNames.HostID)}", _nm);
        }
        
        internal XElement GetProtocolParamsProtocol()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.ProtocolParams)}/{DNS(XmlNames.Version)}/{DNS(XmlNames.Protocol)}", _nm);
        }
        
        internal XElement GetProtocolParamsAuthentication()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.ProtocolParams)}/{DNS(XmlNames.Version)}/{DNS(XmlNames.Authentication)}", _nm);
        }
        
        internal XElement GetProtocolParamsEncryption()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.ProtocolParams)}/{DNS(XmlNames.Version)}/{DNS(XmlNames.Encryption)}", _nm);
        }
        
        internal XElement GetProtocolParamsSignature()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.ProtocolParams)}/{DNS(XmlNames.Version)}/{DNS(XmlNames.Signature)}", _nm);
        }
        
        internal XElement GetProtocolParamsRecovery()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.ProtocolParams)}/{DNS(XmlNames.Recovery)}", _nm);
        }
        
        internal XElement GetProtocolParamsPreValidation()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.ProtocolParams)}/{DNS(XmlNames.PreValidation)}", _nm);
        }
        
        internal XElement GetProtocolParamsX509Data()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.ProtocolParams)}/{DNS(XmlNames.X509Data)}", _nm);
        }
        
        internal XElement GetProtocolParamsClientDataDownload()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.ProtocolParams)}/{DNS(XmlNames.ClientDataDownload)}", _nm);
        }
        
        internal XElement GetProtocolParamsDownloadableOrderData()
        {
            return _doc.XPathSelectElement($"/*/{DNS(XmlNames.ProtocolParams)}/{DNS(XmlNames.DownloadableOrderData)}", _nm);
        }
    }
}