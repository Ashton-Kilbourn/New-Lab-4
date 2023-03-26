
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeroesV2.Tests
{
    public static class Extensions
    {
        public static bool IsOverride(this MethodInfo method)
        {
            return !method.Equals(method.GetBaseDefinition());
        }
    }
    internal class LabTests
    {
        enum Criteria
        {
            NoAttempt = 0,      //0%      F
            Insufficient = 59,  //1-59%   F
            Beginning = 69,     //60-69%  D
            Developing = 79,    //70-79%  C
            Effective = 89,     //80-89%  B
            Exemplary           //90-100% A
        }

        public static void RunTests()
        {
            Console.Clear();
            RunPartATests();
            RunPartBTests();

            Console.WriteLine("\nNOTE: Further review of the code is needed to determine the final grade.");
        }

        #region Test Helpers
        private static void ShowResult(string part, Criteria testCriteria, string message)
        {
            ConsoleColor back = ConsoleColor.Green;
            string title = string.Empty;
            switch (testCriteria)
            {
                case Criteria.NoAttempt:
                    title = "No Attempt 0%";
                    back = ConsoleColor.Red;
                    break;
                case Criteria.Insufficient:
                    title = "Insufficient 1-59%";
                    back = ConsoleColor.Red;
                    break;
                case Criteria.Beginning:
                    title = "Beginning 60-69%";
                    back = ConsoleColor.Yellow;
                    break;
                case Criteria.Developing:
                    title = "Developing 70-79%";
                    back = ConsoleColor.Gray;
                    break;
                case Criteria.Effective:
                    title = "Effective 80-89%";
                    back = ConsoleColor.Blue;
                    break;
                case Criteria.Exemplary:
                    title = "Exemplary 90-100%";
                    back = ConsoleColor.Green;
                    break;
                default:
                    break;
            }
            Console.BackgroundColor = back;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($"{part,30} | {title,20} | ");
            int width = Console.WindowWidth - Console.CursorLeft - 1;
            Console.WriteLine($"{message.PadRight(width)}");

            Console.ResetColor();
        }

        enum MethodProblems
        {
            None,
            NotFound,
            ReturnType,
            Parameters
        }
        enum MethodParts
        {
            Name,
            ReturnType,
            Parameters,
            Execution
        }

        static Dictionary<MethodParts, string> GetInitialMethodResults(string methodName)
        {
            return new Dictionary<MethodParts, string>()
            {
                {MethodParts.Name, $"The {methodName} method does not appear to exist or is not public." },
                {MethodParts.ReturnType, "<not available>" },
                {MethodParts.Parameters, "<not available>" },
                {MethodParts.Execution, "<not available>" }
            };
        }

        enum ClassProblems
        {
            None,
            NotFound
        }

        enum FieldProblems
        {
            None,
            NotFound,
            WrongType
        }

        enum PropertyProblems
        {
            None,
            NotFound,
            WrongType
        }
        enum ClassParts
        {
            Name,
            BaseType,
            Fields,
            Properties,
            Constructors,
            Methods
        }

        static Dictionary<ClassParts, string> GetInitialClassResults(string className)
        {
            return new Dictionary<ClassParts, string>()
            {
                {ClassParts.Name, $"The {className} class does not appear to exist or is not public." },
                {ClassParts.BaseType, "<not available>" },
                {ClassParts.Fields, "<not available>" },
                {ClassParts.Properties, "<not available>" },
                {ClassParts.Constructors, "<not available>" },
                {ClassParts.Methods, "<not available>" }
            };
        }

        private static void ShowClassResult(string part, Criteria testCriteria, Dictionary<ClassParts, string> results, Dictionary<string, (Criteria MethodCriteria, Dictionary<MethodParts, string> MethodResults)> methodResults = null)
        {
            Console.WriteLine();
            ConsoleColor back = ConsoleColor.Green;
            string title = string.Empty;
            switch (testCriteria)
            {
                case Criteria.NoAttempt:
                    title = "No Attempt 0%";
                    back = ConsoleColor.Red;
                    break;
                case Criteria.Insufficient:
                    title = "Insufficient 1-59%";
                    back = ConsoleColor.Red;
                    break;
                case Criteria.Beginning:
                    title = "Beginning 60-69%";
                    back = ConsoleColor.Yellow;
                    break;
                case Criteria.Developing:
                    title = "Developing 70-79%";
                    back = ConsoleColor.Gray;
                    break;
                case Criteria.Effective:
                    title = "Effective 80-89%";
                    back = ConsoleColor.Blue;
                    break;
                case Criteria.Exemplary:
                    title = "Exemplary 90-100%";
                    back = ConsoleColor.Green;
                    break;
                default:
                    break;
            }
            Console.BackgroundColor = back;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{part,-30} | {title,-20}");
            foreach (var result in results)
            {
                Console.WriteLine($"{string.Empty,10}{result.Key,-20}   {result.Value,-20}");
            }
            if (methodResults != null)
            {
                foreach (var methodResult in methodResults)
                {
                    ShowMethodResult(methodResult.Key, methodResult.Value.MethodCriteria, methodResult.Value.MethodResults);
                }
            }
            Console.ResetColor();
        }


        private static void ShowMethodResult(string part, Criteria testCriteria, Dictionary<MethodParts, string> results)
        {
            Console.WriteLine();
            ConsoleColor back = ConsoleColor.Green;
            string title = string.Empty;
            switch (testCriteria)
            {
                case Criteria.NoAttempt:
                    title = "No Attempt 0%";
                    back = ConsoleColor.Red;
                    break;
                case Criteria.Insufficient:
                    title = "Insufficient 1-59%";
                    back = ConsoleColor.Red;
                    break;
                case Criteria.Beginning:
                    title = "Beginning 60-69%";
                    back = ConsoleColor.Yellow;
                    break;
                case Criteria.Developing:
                    title = "Developing 70-79%";
                    back = ConsoleColor.Gray;
                    break;
                case Criteria.Effective:
                    title = "Effective 80-89%";
                    back = ConsoleColor.Blue;
                    break;
                case Criteria.Exemplary:
                    title = "Exemplary 90-100%";
                    back = ConsoleColor.Green;
                    break;
                default:
                    break;
            }
            Console.BackgroundColor = back;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{part,-30} | {title,-20}");
            foreach (var result in results)
            {
                Console.WriteLine($"{string.Empty,10}{result.Key,-20}   {result.Value,-20}");
                //Console.Write(result.Key);
                //Console.CursorLeft = 20;
                //Console.WriteLine(result.Value);
            }
            Console.ResetColor();
        }


        private static MethodProblems EvaluateMethod(string methodName, MethodInfo method, Type expectedReturnType, List<Type> parameterList, ref string message)
        {
            MethodProblems problem = MethodProblems.None;
            //Type heroDBType = typeof(HeroesDB);
            //var method = heroDBType.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            //var methods = heroDBType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            //foreach (var mtd in methods)
            //{
            //    Console.WriteLine($"Method: {mtd.Name}");
            //}
            if (method == null)
            {
                problem = MethodProblems.NotFound;
                message = $"The {methodName} method does not appear to exist or is not public.";
            }
            else
            {
                if (expectedReturnType != method.ReturnType)
                {
                    problem = MethodProblems.ReturnType;
                    message = $"Return type for {methodName} should be {expectedReturnType}.";
                }
                else
                {
                    var parameters = method.GetParameters();
                    if (parameters.Length != parameterList.Count)
                    {
                        problem = MethodProblems.Parameters;
                        message = $"Not the correct number of parameters for {methodName}. Expected: {parameterList.Count}. Actual: {parameters.Length}.";
                    }
                    else
                    {
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            if (parameters[i].ParameterType != parameterList[i])
                            {
                                problem = MethodProblems.Parameters;
                                message = $"{methodName} parameter #{i + 1} is the wrong type. Expected: {parameterList[i]}. Actual: {parameters[i].ParameterType}";
                                break;
                            }
                        }
                    }
                }
            }
            if (problem == MethodProblems.None)
                message = $"{methodName} method signature is correct.";

            return problem;
        }


        private static MethodProblems EvaluateMethod(string methodName, MethodInfo method, Type expectedReturnType, List<Type> parameterList, ref string message, out Dictionary<MethodParts, string> methodResults)
        {
            MethodProblems problem = MethodProblems.None;
            methodResults = GetInitialMethodResults(methodName);
            //Type heroDBType = typeof(HeroesDB);
            //var method = heroDBType.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            //var methods = heroDBType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            //foreach (var mtd in methods)
            //{
            //    Console.WriteLine($"Method: {mtd.Name}");
            //}
            if (method == null)
            {
                problem = MethodProblems.NotFound;
                message = $"The {methodName} method does not appear to exist or is not public.";
            }
            else
            {
                methodResults[MethodParts.Name] = "GOOD";
                if (expectedReturnType != method.ReturnType)
                {
                    problem = MethodProblems.ReturnType;
                    message = $"Return type for {methodName} should be {expectedReturnType}.";
                    methodResults[MethodParts.ReturnType] = message;
                }
                else
                {
                    methodResults[MethodParts.ReturnType] = "GOOD";
                    var parameters = method.GetParameters();
                    message = "GOOD";
                    if (parameters.Length != parameterList.Count)
                    {
                        problem = MethodProblems.Parameters;
                        message = $"Not the correct number of parameters for {methodName}. Expected: {parameterList.Count}. Actual: {parameters.Length}.";
                    }
                    else
                    {
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            if (parameters[i].ParameterType != parameterList[i])
                            {
                                problem = MethodProblems.Parameters;
                                message = $"{methodName} parameter #{i + 1} is the wrong type. Expected: {parameterList[i]}. Actual: {parameters[i].ParameterType}";
                                break;
                            }
                        }
                    }
                    methodResults[MethodParts.Parameters] = message;
                }
            }
            if (problem == MethodProblems.None)
                message = $"{methodName} method signature is correct.";

            return problem;
        }


        private static FieldProblems CheckField(Type typeToCheck, string fieldName, BindingFlags flags, Type fieldType)
        {
            FieldProblems isGood = FieldProblems.None;
            var fieldInfo = typeToCheck.GetField(fieldName, flags);
            if (fieldInfo == null)
            {
                isGood = FieldProblems.NotFound;
            }
            else if (fieldInfo.FieldType != fieldType)
            {
                isGood = FieldProblems.WrongType;
            }
            return isGood;
        }
        private static PropertyProblems CheckProperty(Type typeToCheck, string fieldName, BindingFlags flags, Type propertyType)
        {
            PropertyProblems isGood = PropertyProblems.None;
            var fieldInfo = typeToCheck.GetProperty(fieldName, flags);
            if (fieldInfo == null)
            {
                isGood = PropertyProblems.NotFound;
            }
            else if (fieldInfo.PropertyType != propertyType)
            {
                isGood = PropertyProblems.WrongType;
            }
            return isGood;
        }
        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      //.Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }

        static Assembly _graphicsLib = null;
        private static Assembly GetGraphicsLibAssembly()
        {
            if (_graphicsLib != null)
                return _graphicsLib;

            foreach (var assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {
                Assembly assembly = Assembly.Load(assemblyName);
                if (assemblyName.Name == "GraphicsLibrary")
                {
                    _graphicsLib = assembly;
                    break;
                }
            }
            return _graphicsLib;
        }

        private static Type GetAssemblyType(string typeName)
        {
            GetGraphicsLibAssembly();
            Type typeToreturn = (_graphicsLib != null) ? _graphicsLib.GetType(typeName) : null;
            return typeToreturn;
        }

        #endregion

        #region Part A Tests
        private static void RunPartATests()
        {
            PartA_HighScoreClassTest();
        }
        private static void PartA_HighScoreClassTest()
        {
            Criteria testCriteria = Criteria.NoAttempt;
            string message = string.Empty;
            string className = "HighScore";
            string nameSpace = "WarGame";
            var classResults = GetInitialClassResults(className);
            Dictionary<string, (Criteria MethodCriteria, Dictionary<MethodParts, string> MethodResults)> listOfMethodResults = new();

            Type highScoreClass = Assembly.GetExecutingAssembly().GetType($"{nameSpace}.{className}");
            if (highScoreClass != null)
            {
                classResults[ClassParts.Name] = "GOOD";

                #region Properties Tests
                testCriteria = Criteria.Exemplary;
                PropertyProblems propertyProblems = CheckProperty(highScoreClass, "Name", BindingFlags.Public | BindingFlags.Instance, typeof(string));
                if (propertyProblems == PropertyProblems.NotFound)
                {
                    message = $"The Name property does not exists in {className}.\n";
                    testCriteria = Criteria.Insufficient;
                }
                else if (propertyProblems == PropertyProblems.WrongType)
                {
                    var propertyInfo = highScoreClass.GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
                    message = $"The Name property should be a string in {className} but you defined it as {propertyInfo.PropertyType}.\n";
                    testCriteria = Criteria.Insufficient;
                }

                string propMsg = message;
                propertyProblems = CheckProperty(highScoreClass, "Score", BindingFlags.Public | BindingFlags.Instance, typeof(int));
                if (propertyProblems == PropertyProblems.NotFound)
                {
                    propMsg += $"The Score property does not exists in {className}.\n";
                    testCriteria = Criteria.Insufficient;
                }
                else if (propertyProblems == PropertyProblems.WrongType)
                {
                    var propertyInfo = highScoreClass.GetProperty("Score", BindingFlags.Public | BindingFlags.Instance);
                    propMsg += $"The Score property should be an int in {className} but you defined it as {propertyInfo.PropertyType}.\n";
                    testCriteria = Criteria.Insufficient;
                }
                message += propMsg;
                classResults[ClassParts.Properties] = string.IsNullOrEmpty(propMsg) ? "GOOD" : propMsg;
                #endregion

                #region Constructors Tests

                #endregion


                #region Methods Tests
                Type list = typeof(List<>);
                Type[] args = { highScoreClass };
                Type listHighScore = list.MakeGenericType(args);

                string methodName;
                MethodInfo method;
                MethodProblems methodProblems;
                List<Type> expectedParams;
                Dictionary<MethodParts, string> methodResults;
                Criteria methodCriteria;
                bool hasMethodProblems = false;

                #region LoadHighScore
                methodCriteria = Criteria.NoAttempt;
                methodName = "LoadHighScores";
                method = highScoreClass.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                expectedParams = new List<Type>() { typeof(string) };
                methodProblems = EvaluateMethod(methodName, method, listHighScore, expectedParams, ref message, out methodResults);

                if (methodProblems == MethodProblems.None)
                {
                    //
                    // Does the method work correctly
                    //
                    var highScores = Activator.CreateInstance(listHighScore);
                    highScores = method.Invoke(null, new object[] { "HighScores.json" });// as listHighScore;
                    var returnedList = Convert.ChangeType(highScores, listHighScore) as ICollection;

                    if (returnedList != null)
                    {
                        if (returnedList.Count == 10)
                        {
                            methodCriteria = Criteria.Exemplary;
                            methodResults[MethodParts.Execution] = "GOOD";
                        }
                        else
                        {
                            methodCriteria = Criteria.Developing;
                            methodResults[MethodParts.Execution] = "The number of highscores returned was not 10.";
                        }
                    }
                }
                else
                {
                    hasMethodProblems = true;
                    switch (methodProblems)
                    {
                        case MethodProblems.NotFound:
                            methodCriteria = Criteria.NoAttempt;
                            break;
                        case MethodProblems.ReturnType:
                        case MethodProblems.Parameters:
                            methodCriteria = Criteria.Insufficient;
                            break;
                        default:
                            break;
                    }
                }
                listOfMethodResults[methodName] = (methodCriteria,methodResults);
                #endregion

                #region SaveHighScores
                methodCriteria = Criteria.NoAttempt;
                methodName = "SaveHighScores";
                method = highScoreClass.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                expectedParams = new List<Type>() { typeof(string), listHighScore };
                methodProblems = EvaluateMethod(methodName, method, typeof(void), expectedParams, ref message, out methodResults);

                if (methodProblems == MethodProblems.None)
                {
                    methodCriteria = Criteria.Exemplary;
                    //
                    // Does the method work correctly
                    //
                }
                else
                {
                    hasMethodProblems = true;
                    switch (methodProblems)
                    {
                        case MethodProblems.NotFound:
                            methodCriteria = Criteria.NoAttempt;
                            break;
                        case MethodProblems.ReturnType:
                        case MethodProblems.Parameters:
                            methodCriteria = Criteria.Insufficient;
                            break;
                        default:
                            break;
                    }
                }
                listOfMethodResults[methodName] = (methodCriteria, methodResults);
                #endregion

                #region ShowHighScores
                methodCriteria = Criteria.NoAttempt;
                methodName = "ShowHighScores";
                method = highScoreClass.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                expectedParams = new List<Type>() { listHighScore };
                methodProblems = EvaluateMethod(methodName, method, typeof(void), expectedParams, ref message, out methodResults);

                if (methodProblems == MethodProblems.None)
                {
                    methodCriteria = Criteria.Exemplary;
                    //
                    // Does the method work correctly
                    //
                }
                else
                {
                    hasMethodProblems = true;
                    switch (methodProblems)
                    {
                        case MethodProblems.NotFound:
                            methodCriteria = Criteria.NoAttempt;
                            break;
                        case MethodProblems.ReturnType:
                        case MethodProblems.Parameters:
                            methodCriteria = Criteria.Insufficient;
                            break;
                        default:
                            break;
                    }
                }
                listOfMethodResults[methodName] = (methodCriteria, methodResults);
                #endregion

                classResults[ClassParts.Methods] = !hasMethodProblems ? "GOOD" : "method problems...";
                message += classResults[ClassParts.Methods];
                #endregion

                if (testCriteria == Criteria.Exemplary)
                    message += $"{className} is correct.";
            }
            else
            {
                message = $"The {className} struct does not appear to exist or is not public.";
            }
            ShowClassResult($"Part A: {className} Class", testCriteria, classResults, listOfMethodResults);
        }
        #endregion

        #region Part B Tests

        private static void RunPartBTests()
        {
            PartB_CardWarsClassTest();
        }
        private static void PartB_CardWarsClassTest()
        {
            Criteria testCriteria = Criteria.NoAttempt;
            string message = string.Empty;
            string className = "CardWars";
            string nameSpace = "WarGame";
            var classResults = GetInitialClassResults(className);
            Dictionary<string, (Criteria MethodCriteria, Dictionary<MethodParts, string> MethodResults)> listOfMethodResults = new();

            Type cardWarsClass = Assembly.GetExecutingAssembly().GetType($"{nameSpace}.{className}");
            Type highScoreClass = Assembly.GetExecutingAssembly().GetType($"{nameSpace}.HighScore");
            if (cardWarsClass != null && highScoreClass != null)
            {
                testCriteria = Criteria.Exemplary;
                classResults[ClassParts.Name] = "GOOD";

                #region Properties Tests
                #endregion

                #region Constructors Tests
                #endregion


                #region Methods Tests
                Type list = typeof(List<>);
                Type[] args = { highScoreClass };
                Type listHighScore = list.MakeGenericType(args);

                string methodName;
                MethodInfo method;
                MethodProblems methodProblems;
                List<Type> expectedParams;
                Dictionary<MethodParts, string> methodResults;
                Criteria methodCriteria;
                bool hasMethodProblems = false;

                #region LoadCards
                methodCriteria = Criteria.NoAttempt;
                methodName = "LoadCards";
                method = cardWarsClass.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                expectedParams = new List<Type>() { typeof(string) };
                methodProblems = EvaluateMethod(methodName, method, typeof(List<string>), expectedParams, ref message, out methodResults);

                if (methodProblems == MethodProblems.None)
                {
                    //
                    // Does the method work correctly
                    //
                    List<string> testData = method.Invoke(null, new object[] { "cards.csv" }) as List<string>;
                    if (testData != null)
                    {
                        if (testData.Count == 52)
                        {
                            List<string> goodData = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(Path.Combine("Tests", "loadCardsData.json")));
                            if (goodData.SequenceEqual(testData))
                            {
                                methodCriteria = Criteria.Exemplary;
                                methodResults[MethodParts.Execution] = "GOOD";
                            }
                            else
                            {
                                methodCriteria = Criteria.Developing;
                                methodResults[MethodParts.Execution] = $"The data returned is not correct.\nGOOD: {string.Join(',', goodData)}\nYOURS:{string.Join(',', testData)}";
                            }
                        }
                        else
                        {
                            methodCriteria = Criteria.Developing;
                            methodResults[MethodParts.Execution] = $"The number of cards returned ({testData.Count}) is not 52.";
                        }
                    }
                    else
                    {
                        methodCriteria = Criteria.Insufficient;
                        methodResults[MethodParts.Execution] = $"The returned data was null.";
                    }
                }
                else
                {
                    hasMethodProblems = true;
                    switch (methodProblems)
                    {
                        case MethodProblems.NotFound:
                            methodCriteria = Criteria.NoAttempt;
                            break;
                        case MethodProblems.ReturnType:
                        case MethodProblems.Parameters:
                            methodCriteria = Criteria.Insufficient;
                            break;
                        default:
                            break;
                    }
                    testCriteria = Criteria.Insufficient;
                }
                listOfMethodResults[methodName] = (methodCriteria, methodResults);
                #endregion

                #region PlayGame
                methodCriteria = Criteria.NoAttempt;
                methodName = "PlayGame";
                method = cardWarsClass.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                expectedParams = new List<Type>() { typeof(List<string>), listHighScore, typeof(string) };
                methodProblems = EvaluateMethod(methodName, method, typeof(void), expectedParams, ref message, out methodResults);

                if (methodProblems == MethodProblems.None)
                {
                    methodCriteria = Criteria.Exemplary;
                    //
                    // Does the method work correctly
                    //
                }
                else
                {
                    hasMethodProblems = true;
                    switch (methodProblems)
                    {
                        case MethodProblems.NotFound:
                            methodCriteria = Criteria.NoAttempt;
                            break;
                        case MethodProblems.ReturnType:
                        case MethodProblems.Parameters:
                            methodCriteria = Criteria.Insufficient;
                            break;
                        default:
                            break;
                    }
                    testCriteria = Criteria.Insufficient;
                }
                listOfMethodResults[methodName] = (methodCriteria, methodResults);
                #endregion

                classResults[ClassParts.Methods] = !hasMethodProblems ? "GOOD" : "method problems...";
                message += classResults[ClassParts.Methods];
                #endregion

                if (testCriteria == Criteria.Exemplary)
                    message += $"{className} is correct.";
            }
            else
            {
                message = $"The {className} struct does not appear to exist or is not public.";
            }
            ShowClassResult($"Part B: {className} Class", testCriteria, classResults, listOfMethodResults);
        }
        #endregion
    }
}
