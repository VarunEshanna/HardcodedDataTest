using System;
using System.Collections;
using System.Collections.Generic;
using LegalService;
using NUnit.Framework;
using TestFrameworkLib;
using TestFrameworkLib.beans;

namespace TestFramework
{

    public class MyDataClass
    {

        

        private static int CalculateDataSetParams(Dictionary<string, object> names, ref int arguments)
        {
            int dataSets;
            foreach (KeyValuePair<String, Object> entry in names)
            {
                String objectName = entry.Key;
                if (objectName.IndexOf('1') == 7)
                {
                    arguments++;
                }
                else
                {
                    break;
                }

            }
            dataSets = names.Count / arguments;
            return dataSets;
            }

           

            public static IEnumerable AutoAutoAssertion1(String MultipleDataSetName)
        {
            String[] DataSetNames = MultipleDataSetName.Split(',');
            for(int j=0; j < DataSetNames.Length; j++)
            {
                String DataSetName = DataSetNames[j];
                MongoDbConnection mongoDbConnection = new MongoDbConnection();
                //ClassDetails classDetails = mongoDbConnection.getClassDetails(DataSetName);
                ClassDetails classDetails = MyDataClass.getClassDetails(DataSetName);
                //Dictionary<String, Object> reqNames = mongoDbConnection.getRequestData(DataSetName, "Request");
                Dictionary<String, Object> reqNames = MyDataClass.getRequestNames(DataSetName);
                //Dictionary<String, Object> respNames = mongoDbConnection.getRequestData(DataSetName, "Response");
                Dictionary<String, Object> respNames = MyDataClass.getResponseNames(DataSetName);

                int dataSets = 0;
                int arguments = 0;
                dataSets = CalculateDataSetParams(reqNames, ref arguments);

                for (int i = 1; i < dataSets + 1; i++)
                {
                    //TODO create testcases objects using reflection
                    if (arguments == 1)
                    {
                        yield return new TestCaseData(classDetails, reqNames["Request" + i + "1"]).Returns(respNames["Response" + i]);
                    }
                    else if (arguments == 2)
                    {
                        yield return new TestCaseData(classDetails, reqNames["Request" + i + "1"], reqNames["Request" + i + "2"]).Returns(respNames["Response" + i]);
                    }
                    else if (arguments == 3)
                    {
                        yield return new TestCaseData(classDetails, reqNames["Request" + i + "1"], reqNames["Request" + i + "2"], reqNames["Request" + i + "3"]).Returns(respNames["Response" + i]);
                    }
                }
            }
        }


        

        

        public static ClassDetails getClassDetails (String DatasetName)
        {
            ClassDetails classDetails1 = new ClassDetails();
            classDetails1.DataSet = "Adobe Corporate Entity Records";
            classDetails1.ClassName = "LegalService.AgreementInitiationService";
            classDetails1.MethodName = "GetAdobeCorporateEntity";
            List<String> requestObjects1 = new List<String>();
            requestObjects1.Add("LegalService.GetAdobeCorporateEntityRequest");
            classDetails1.entityResponeData = requestObjects1;
            classDetails1.ResponseType = "LegalService.GetAdobeCorporateEntityResponse";


            ClassDetails classDetails = new ClassDetails();
            classDetails.DataSet = "Adobe Contract Class Records";
            classDetails.ClassName = "LegalService.AgreementInitiationService";
            classDetails.MethodName = "GetAgreementContractClass";
            List<String> requestObjects = new List<String>();
            requestObjects.Add("LegalService.GetAgreementContractClassRequest");
            classDetails.entityResponeData = requestObjects;
            classDetails.ResponseType = "LegalService.GetAgreementContractClassResponse";

            if (DatasetName.Equals(classDetails1.DataSet))
            {
                return classDetails1;
            }
            else
            {
                return classDetails;
            }
        }


        public static Dictionary<String, Object> getRequestNames(String dataSetName)
        {
            Dictionary<String, Object> reqObj = new Dictionary<String, Object>();
            if (dataSetName.Equals("Adobe Corporate Entity Records"))
            {
                GetAdobeCorporateEntityRequest GetAdobeCorporateEntityRequest1 = new GetAdobeCorporateEntityRequest();
                GetAdobeCorporateEntityRequest1.AccountCountry = "US";
                GetAdobeCorporateEntityRequest1.AgreementType = "DMA";
                GetAdobeCorporateEntityRequest1.MarketSegment = "Commercial";
                GetAdobeCorporateEntityRequest1.isCorporateEntityOverride = true;
                reqObj.Add("Request11", GetAdobeCorporateEntityRequest1);

                GetAdobeCorporateEntityRequest GetAdobeCorporateEntityRequest2 = new GetAdobeCorporateEntityRequest();
                GetAdobeCorporateEntityRequest2.AccountCountry = "UK";
                GetAdobeCorporateEntityRequest2.AgreementType = "DMA";
                GetAdobeCorporateEntityRequest2.MarketSegment = "Commercial";
                GetAdobeCorporateEntityRequest2.isCorporateEntityOverride = true;
                reqObj.Add("Request21", GetAdobeCorporateEntityRequest2);
            }else if (dataSetName.Equals("Adobe Contract Class Records"))
            {
                GetAgreementContractClassRequest getAgreementContractClassRequest1 = new GetAgreementContractClassRequest();
                getAgreementContractClassRequest1.AgreementType = "DMA";
                reqObj.Add("Request11", getAgreementContractClassRequest1);

                GetAgreementContractClassRequest getAgreementContractClassRequest2 = new GetAgreementContractClassRequest();
                getAgreementContractClassRequest2.AgreementType = "NDA";
                reqObj.Add("Request21", getAgreementContractClassRequest2);
            }
            return reqObj;
        }


        public static Dictionary<String, Object> getResponseNames(String dataSetName)
        {
            Dictionary<String, Object> reqObj = new Dictionary<String, Object>();
            if (dataSetName.Equals("Adobe Corporate Entity Records"))
            {
                GetAdobeCorporateEntityResponse GetAdobeCorporateEntityResponse1 = new GetAdobeCorporateEntityResponse();
                GetAdobeCorporateEntityResponse1.AdobeCorporateEntity = "ADUS";
                reqObj.Add("Response1", GetAdobeCorporateEntityResponse1);
                GetAdobeCorporateEntityResponse GetAdobeCorporateEntityResponse2 = new GetAdobeCorporateEntityResponse();
                GetAdobeCorporateEntityResponse2.AdobeCorporateEntity = "ADIR";
                reqObj.Add("Response2", GetAdobeCorporateEntityResponse2);
            }
            else if (dataSetName.Equals("Adobe Contract Class Records"))
            {
                GetAgreementContractClassResponse getAgreementContractClassResponse1 = new GetAgreementContractClassResponse();
                getAgreementContractClassResponse1.ContractClass = "RG";
                reqObj.Add("Response1", getAgreementContractClassResponse1);

                GetAgreementContractClassResponse getAgreementContractClassResponse2 = new GetAgreementContractClassResponse();
                getAgreementContractClassResponse2.ContractClass = "NRG";
                reqObj.Add("Response2", getAgreementContractClassResponse2);
            }
            return reqObj;
        }

    }
}
