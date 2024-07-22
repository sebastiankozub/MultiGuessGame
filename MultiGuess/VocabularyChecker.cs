namespace MultiGuess
{
    /*  
    Class VocabularyChecker not changed - code review only:
    
        1. using instead of try-finally

        2. crate interface
            interface IVocabularReader
            {
                List<string> Read();
            }
            to avoid breaking SRP(SOLID) and give use possibility to quickly change source of word's list

        3. inject class implementing IVocabularReader through constructor

        4. delete
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            to decouple code of view (Console.WriteLine()) from business logic of file reading
            catch exception higher where it is able to decide how to service it in application logic

        5. use Dictionary, HashSet or BST for faster search - efficiency tests suggested to have balance between loading and search

        6. check if strings do not repeat when loading
    */


    internal class VocabularyChecker : IVocabularyChecker
    {
        List<string>? stringList;

        public VocabularyChecker()
        {
            StreamReader? reader = null;
            try
            {
                reader = new StreamReader(new FileStream("wordlist.txt", FileMode.OpenOrCreate));

                var content = reader.ReadToEndAsync();

                stringList = content.Result.Split('\n').ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                reader?.Dispose();
            }
        }

        public bool Exists(string word)
        {
            return stringList?.Contains(word) == true;
        }
    }
}
