using Lab07_Library;
using System.Reflection; //пространство имен для работы с метаданными сборок (assembly) и типами.
using System.Text;
using System.Xml.Linq; //пространство имен для работы с XML.

namespace Lab07_App;

abstract class Program
{
    static void Main()
    {
        Assembly asm = Assembly.Load("Lab07_Library"); //создается объект класса Assembly, который представляет собой сборку "Lab07_Library". Метод Load("Lab07_Library") загружает указанную сборку по ее имени
        var types = asm.GetExportedTypes(); //получаем типы из загруженной сборки и сохраняем их в types
        XElement program = new XElement("Program"); //создаём новый XML-документ
        foreach (var type in types)
        {
            XElement element = new XElement(type.Name); //создаём новый XML-документ с именем текущего типа
            if (type.IsEnum) //проверка текущего типа на перечисление
            {
                element.Add(new XElement("MyComment", type.GetCustomAttribute<MyComment>()!.Comment)); //если тип-это перечисление, то добавляем в элемент комментарий, полученный из атрибута MyComment
                XElement enumValues = new XElement("Values"); //создаём переменную для содержания перечисления
                foreach (var t in type.GetEnumValues()) //цикл по перечислению
                {
                    enumValues.Add(t + " "); //добавляем кажде перечисление в "Values"
                }
                element.Add(enumValues); //добавляем элемент "Values" к текущему элементу
            }
            else
            {
                if (type.Name == "MyComment")
                {
                    element.Add(new XElement("Properties", from t in type.GetProperties() select t.Name + " "), //если тип имеет имя "MyComment", добавляем в элемент "Properties" список имен его свойств
                        new XElement("Constructors", from t in type.GetConstructors() select t.DeclaringType + " ")); //добавляем в элемент "Constructors" список имен конструкторов
                }
                else
                {
                    element.Add(new XElement("MyComment", type.GetCustomAttribute<MyComment>()!.Comment), //добавляем комментарий из атрибута MyComment
                        new XElement("Properties", from t in type.GetProperties() select t.Name + " "), //добавляем в элемент "Properties" список имен свойств
                        new XElement("Constructors", from t in type.GetConstructors() select t.DeclaringType), //добавляем в элемент "Constructors" список имен конструкторов
                        new XElement("Methods",                                                               //добавляем элемент "Methods" для методов данного типа
                            from t in type.GetMethods()
                            where (!t.Name.StartsWith("get_") && !t.Name.StartsWith("set_"))
                            select t.Name + " "));
                }
            }

            program.Add(element);
        }

        XDocument xDocument = new XDocument(program);

        xDocument.Save(@"C:\Users\user\source\repos\Lab 07\Lab7.xml"); //сохранение файла по указанному пути
    }
}