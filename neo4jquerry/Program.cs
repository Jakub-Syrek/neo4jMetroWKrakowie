using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Neo4j.Driver.V1;

namespace neo4jquerry
{
    class Program
    {
        private static string _login = WprowadzKredki();

        private static string _pass = WprowadzHaslo();
        static public string WprowadzKredki()
        {
            Console.WriteLine("Wprowadz Neo4j db Username");
            var input = Console.ReadLine();
            if (input.ToString() != null)
            {
                return input.ToString();
            }
            else
            {
                Console.WriteLine("Wprowadz Neo4j db Username !");
                var input1 = Console.ReadLine();
                return input1;
            }
        }
        static public  string WprowadzHaslo()
        {
            Console.WriteLine("Wprowadz Neo4j db pass");
            var pwd = new SecureString();
            string _pwd = "";
            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    if (pwd.Length > 0)
                    {
                        pwd.RemoveAt(pwd.Length - 1);

                        Console.Write("\b \b");
                    }
                }
                else if (i.KeyChar != '\u0000') 
                {
                    pwd.AppendChar(i.KeyChar);
                    _pwd += $"{i.KeyChar}";  
                    Console.Write("*");
                }
            }
            return _pwd;
        }     

        
   
        static public string WybierzStacjeStartowa()
        {
            using (var _driver = GraphDatabase.Driver("bolt://localhost", AuthTokens.Basic(_login, _pass)))                  
            {
                using (var session = _driver.Session())
                {
                    var result = session.Run("CALL algo.allShortestPaths.stream('cost',{nodeQuery:'Loc',defaultValue:1.0}) YIELD sourceNodeId, targetNodeId, distance WITH sourceNodeId, targetNodeId, distance WHERE algo.isFinite(distance) = true MATCH(source: Loc) WHERE id(source) = sourceNodeId MATCH(target: Loc) WHERE id(target) = targetNodeId WITH source, target, distance WHERE source <> target   RETURN DISTINCT source.name AS source, distance ORDER BY distance DESC LIMIT 1000");

                    List<string> lista = new List<string>();
                    foreach (var record in result)
                    {
                        if (lista.Contains($"{record[0]}"))
                        { }
                        else
                        {
                            lista.Add($"{record[0]}");
                        }                  
                    }

                    Console.WriteLine();
                    
                    

                    

                    Console.WriteLine();

                    Console.WriteLine("Wprowadz nazwe jednej ze stacji startowych lub wpisz 'lista' by wyswietlic dostepne przystanki:");
                    var input = "";
                    
                    if(lista.Contains(input) != true)
                    {
                        input = Console.ReadLine();
                        if (input == "lista")
                        {
                            Console.WriteLine("Nastepujace stacje sa dostepne w wyszukiwaniu:");

                            Console.WriteLine();

                            foreach (var item in lista)
                            {
                                Console.WriteLine($"{item.ToString()}");
                            }


                        }
                    }
                    if (lista.Contains(input) != true)
                    {
                        input = Console.ReadLine();
                        if (input == "lista")
                        {
                            Console.WriteLine("Nastepujace stacje sa dostepne w wyszukiwaniu:");

                            Console.WriteLine();

                            foreach (var item in lista)
                            {
                                Console.WriteLine($"{item.ToString()}");
                            }


                        }
                    }
                    if (lista.Contains(input) != true)
                    {
                        input = Console.ReadLine();
                        if (input == "lista")
                        {
                            Console.WriteLine("Nastepujace stacje sa dostepne w wyszukiwaniu:");

                            Console.WriteLine();

                            foreach (var item in lista)
                            {
                                Console.WriteLine($"{item.ToString()}");
                            }


                        }
                    }
                    return input;
                }
            }
        }
        static public string WybierzStacjeDocelowa()
        {
            using (var _driver = GraphDatabase.Driver("bolt://localhost", AuthTokens.Basic(_login, _pass)))
            {
                using (var session = _driver.Session())
                {
                    var result = session.Run("CALL algo.allShortestPaths.stream('cost',{nodeQuery:'Loc',defaultValue:1.0}) YIELD sourceNodeId, targetNodeId, distance WITH sourceNodeId, targetNodeId, distance WHERE algo.isFinite(distance) = true MATCH(source: Loc) WHERE id(source) = sourceNodeId MATCH(target: Loc) WHERE id(target) = targetNodeId WITH source, target, distance WHERE source <> target   RETURN DISTINCT source.name AS source, distance ORDER BY distance DESC LIMIT 1000");

                    List<string> lista = new List<string>();
                    foreach (var record in result)
                    {
                        if (lista.Contains($"{record[0]}"))
                        { }
                        else
                        {
                            lista.Add($"{record[0]}");
                        }                     
                    }

                    foreach (var item in lista)
                    {
                      //  Console.WriteLine($"{item.ToString()}");
                    }
                    Console.WriteLine("Wprowadz nazwe jednej ze stacji docelowych lub wpisz 'lista' by wyswietlic dostepne przystanki:");
                    var input = "";

                    if (lista.Contains(input) != true)
                    {
                        input = Console.ReadLine();
                        if (input == "lista")
                        {
                            Console.WriteLine("Nastepujace stacje sa dostepne w wyszukiwaniu:");

                            Console.WriteLine();

                            foreach (var item in lista)
                            {
                                Console.WriteLine($"{item.ToString()}");
                            }

                            Console.WriteLine("Wprowadz nazwe jednej ze stacji docelowych lub wpisz 'lista' by wyswietlic dostepne przystanki:");
                        }
                    }
                    if (lista.Contains(input) != true)
                    {
                       

                        input = Console.ReadLine();
                        if (input == "lista")
                        {
                            foreach (var item in lista)
                            {
                                Console.WriteLine($"{item.ToString()}");
                            }


                        }
                    }
                    if (lista.Contains(input) != true)
                    {
                        input = Console.ReadLine();
                        if (input == "lista")
                        {
                            Console.WriteLine("Nastepujace stacje sa dostepne w wyszukiwaniu:");

                            Console.WriteLine();

                            foreach (var item in lista)
                            {
                                Console.WriteLine($"{item.ToString()}");
                            }


                        }
                    }
                    return input;
                }
            }
        }
        static public void WczytajBazeNeo4j()
        {
            using (var _driver = GraphDatabase.Driver("bolt://localhost", AuthTokens.Basic(_login, _pass)))
            {
                using (var session = _driver.Session())
                {
                    session.Run(@"MERGE (a:Loc {name: 'Biprostal'})
                                    MERGE (b:Loc {name: 'placInwalidow'})
                                    MERGE (c:Loc {name: 'Bagatella'})
                                    MERGE (d:Loc {name: 'DzielnicaVI'})
                                    MERGE (e:Loc {name: 'Olszanica'})
                                    MERGE (f:Loc {name: 'Dworzec'})
                                    MERGE (g:Loc {name: 'Lubicz'})
                                    MERGE (h:Loc {name: 'Szkieletor'})
                                    MERGE (i:Loc {name: 'Rakowice'})
                                    MERGE (j:Loc {name: 'Dywizjonu'})
                                    MERGE (k:Loc {name: 'Czyzyny'})
                                    MERGE (l:Loc {name: 'CentrumE'})
                                    MERGE (m:Loc {name: 'Ujastek'})
                                    MERGE (n:Loc {name: 'Mistrzejowice'})
                                    MERGE (o:Loc {name: 'Oswiecenia'})
                                    MERGE (p:Loc {name: 'ParkWodny'})
                                    MERGE (q:Loc {name: 'Olsza'})
                                    MERGE (r:Loc {name: 'Nowy Kleparz'})
                                    MERGE (s:Loc {name: 'Kazimierz'})
                                    MERGE (t:Loc {name: 'Kapelanka'})
                                    MERGE (u:Loc {name: 'Tesco'})
                                    MERGE (w:Loc {name: 'Kobierzyn'})
                                    MERGE (x:Loc {name: 'Skotnicka'})
                                    MERGE (y:Loc {name: 'Skawina'})
                                    MERGE (d)-[:ROAD {cost:3}]->(a)
                                    MERGE (a)-[:ROAD {cost:2}]->(b)
                                    MERGE (e)-[:ROAD {cost:5}]->(d)
                                    MERGE (c)-[:ROAD {cost:2}]->(f)
                                    MERGE (f)-[:ROAD {cost:1}]->(g)
                                    MERGE (g)-[:ROAD {cost:1}]->(h)
                                    MERGE (h)-[:ROAD {cost:2}]->(i)
                                    MERGE (i)-[:ROAD {cost:2}]->(j)
                                    MERGE (d)-[:ROAD {cost:5}]->(e)
                                    MERGE (a)-[:ROAD {cost:3}]->(d)
                                    MERGE (b)-[:ROAD {cost:2}]->(a)
                                    MERGE (b)-[:ROAD {cost:1}]->(c)
                                    MERGE (c)-[:ROAD {cost:1}]->(b)
                                    MERGE (f)-[:ROAD {cost:2}]->(c)
                                    MERGE (g)-[:ROAD {cost:1}]->(f)
                                    MERGE (h)-[:ROAD {cost:1}]->(g)
                                    MERGE (i)-[:ROAD {cost:1}]->(h)
                                    MERGE (j)-[:ROAD {cost:2}]->(i)
                                    MERGE (j)-[:ROAD {cost:2}]->(k)
                                    MERGE (k)-[:ROAD {cost:2}]->(j)
                                    MERGE (k)-[:ROAD {cost:2}]->(l)
                                    MERGE (l)-[:ROAD {cost:2}]->(k)
                                    MERGE (l)-[:ROAD {cost:3}]->(m)
                                    MERGE (m)-[:ROAD {cost:3}]->(l)
                                    MERGE (n)-[:ROAD {cost:2}]->(o)
                                    MERGE (o)-[:ROAD {cost:1}]->(p)
                                    MERGE (p)-[:ROAD {cost:2}]->(q)
                                    MERGE (q)-[:ROAD {cost:2}]->(r)
                                    MERGE (r)-[:ROAD {cost:2}]->(b)
                                    MERGE (b)-[:ROAD {cost:2}]->(r)
                                    MERGE (r)-[:ROAD {cost:2}]->(q)
                                    MERGE (q)-[:ROAD {cost:2}]->(p)
                                    MERGE (p)-[:ROAD {cost:1}]->(o)
                                    MERGE (o)-[:ROAD {cost:2}]->(n)
                                    MERGE (b)-[:ROAD {cost:1}]->(s)
                                    MERGE (s)-[:ROAD {cost:3}]->(t)
                                    MERGE (t)-[:ROAD {cost:2}]->(u)
                                    MERGE (u)-[:ROAD {cost:3}]->(w)
                                    MERGE (w)-[:ROAD {cost:3}]->(x)
                                    MERGE (x)-[:ROAD {cost:4}]->(y)
                                    MERGE (y)-[:ROAD {cost:4}]->(x)
                                    MERGE (x)-[:ROAD {cost:3}]->(w)
                                    MERGE (w)-[:ROAD {cost:3}]->(u)
                                    MERGE (u)-[:ROAD {cost:2}]->(t)
                                    MERGE (t)-[:ROAD {cost:3}]->(s)
                                    MERGE (s)-[:ROAD {cost:1}]->(b)");
                
                }
            }
        }
        static void Main(string[] args)
        {

            WczytajBazeNeo4j();

            var Start = WybierzStacjeStartowa();

            var Stop = WybierzStacjeDocelowa();

            Console.WriteLine();

            using (var driver = GraphDatabase.Driver("bolt://localhost", AuthTokens.Basic(_login, _pass)))
            {
                using (var session = driver.Session())
                {
                    string st = $@"MATCH (start:Loc{{name:'{Start}'}}), (end:Loc{{name:'{Stop}'}}) CALL algo.shortestPath(start, end, 'cost',{{write:true,writeProperty:'sssp'}}) YIELD writeMillis,loadMillis,nodeCount, totalCost RETURN writeMillis,loadMillis,nodeCount,totalCost";
                    var result = session.Run(st);

                    foreach (var record in result)
                        Console.WriteLine($"{record["nodeCount"].As<string>()} przystankow i {record["totalCost"].As<string>()} jednostek czasowych do pokonania trasy z {Start} do {Stop}");
                }
            }
            

            using (var driver = GraphDatabase.Driver("bolt://localhost", AuthTokens.Basic(_login, _pass)))
            {
                using (var session = driver.Session())
                {
                    string st = $@"MATCH(n: Loc {{ name: '{Start}'}}) CALL algo.shortestPath.deltaStepping.stream(n, 'cost', 3.0) YIELD nodeId, distance RETURN algo.asNode(nodeId).name AS destination, distance ORDER BY distance ASC";

                    var result = session.Run(st);
                    Console.WriteLine();
                    foreach (var record in result)
                        Console.WriteLine($"{record["destination"].As<string>()} {record["distance"].As<string>()}");
                }
            }
            Console.ReadKey();


        }
    }
}
