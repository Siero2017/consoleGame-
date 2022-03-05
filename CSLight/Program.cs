using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSLight
{   
    class Program
    {
        
        static void Main(string[] args)
        {
            double bossHealth = 900;
            double heroHealth = 600;

            double bossBaseDamage = 150;
            double heroBaseDamage = 100;

            double bossCritChance = 30;
            double heroCritChance = 60;
            double critDamage = 2;

            int magickShield = 20;

            double advanceShootDamage = 200;
            double advanceShootDamageChance = 50;

            double fastStepsHeal = 100;
            bool willFastStepsKarma = false;
            double karmaForHeal = 10;

            double headShootDamage = 100;

            Random luckyNumberForCrit = new Random();            
            Random luckyNumberForHit = new Random();
            int startIndexForRandom = 0;
            int endIndexForRandom = 101;

            string heroActivity;
            double takeDamageOnBoss;
            double takeDamageOnHero;

            Console.WriteLine("Сейчас вам предстоит сразиться с боссом ущелья бездны!\n\n");

            var characteristicsHero = ($"Хар-ки героя: \nЗдоровье - {heroHealth}" +
                $"\nБазовый урон (суммируется с уроном умений) - {heroBaseDamage}" +
                $"\nКрит.шанс (увеличивает урон в {critDamage} раз) - {heroCritChance}%");

            var characteristicsBoss = ($"Хар-ки босса: \nЗдоровье - {bossHealth}" +
                $"\nБазовый урон (суммируется с уроном умений) - {bossBaseDamage}" +
                $"\nКрит.шанс (увеличивает урон в {critDamage} раз) - {bossCritChance}%");

            var heroSpels = "\nВаши умения:\n" +
                $"[Пассивно]Магический щит -  с шансом в {magickShield}% защищает от следующей атаки врага\n" +
                $"[1]Выстрел на опережение - после подготовки вы выстреливаете из винтовки и наносите {advanceShootDamage} урона" +
                $"(шанс попадания {advanceShootDamageChance}%)\n" +
                $"[2]Шустрые ноги - вы отбегаете от противника на дистанцию и восстанавливаете {fastStepsHeal} здоровья" +
                $"(шанс попадания при следующей атаке -{karmaForHeal}%)\n" +
                "[3]Выстрел в голову - обычный выстрел из старых револьверов, который наносит базовый урон\n";

            Console.WriteLine(characteristicsHero);
            Console.WriteLine();
            Console.WriteLine(characteristicsBoss);
            Console.WriteLine(heroSpels);

            Console.WriteLine("\nЖелаете принять поединок? Да/Нет");
            bool startGame = Console.ReadLine() == "Да";            

            if (startGame)
            {
                Console.Clear();
                Console.WriteLine(@"Напиши ""Сдаться"" если хочешь завершить игру");                

                while (startGame)
                {
                    Console.WriteLine(characteristicsHero);
                    Console.WriteLine();
                    Console.WriteLine(characteristicsBoss);
                    Console.WriteLine(heroSpels);

                    Console.WriteLine("Чем будем атаковать?");
                    heroActivity = Console.ReadLine();

                    switch(heroActivity)
                    {
                        case "1":
                            if (willFastStepsKarma) 
                            {
                                willFastStepsKarma = false;
                                takeDamageOnBoss = Convert.ToDouble(luckyNumberForHit.Next(startIndexForRandom, endIndexForRandom) <= advanceShootDamageChance - karmaForHeal) * advanceShootDamage;
                            }
                            else
                                takeDamageOnBoss = Convert.ToDouble(luckyNumberForHit.Next(startIndexForRandom, endIndexForRandom) <= advanceShootDamageChance) * advanceShootDamage;

                            if (luckyNumberForCrit.Next(startIndexForRandom, endIndexForRandom) <= heroCritChance)
                            {
                                takeDamageOnBoss *= critDamage;
                            }                                

                            bossHealth -= takeDamageOnBoss;

                            Console.WriteLine($"Нанесено урона: { takeDamageOnBoss }");

                            break;
                        case "2":
                            heroHealth += fastStepsHeal;
                            willFastStepsKarma = true;

                            break;
                        case "3":
                            if (willFastStepsKarma) 
                            {
                                willFastStepsKarma = false;
                                takeDamageOnBoss = headShootDamage * Convert.ToDouble(luckyNumberForHit.Next(startIndexForRandom, endIndexForRandom) <= karmaForHeal);
                            }
                            else
                            {
                                takeDamageOnBoss = headShootDamage;
                            }                                

                            if (luckyNumberForCrit.Next(startIndexForRandom, endIndexForRandom) <= heroCritChance)
                            {
                                takeDamageOnBoss *= critDamage;
                            }
                            
                            bossHealth -= takeDamageOnBoss;
                            Console.WriteLine($"Нанесено урона: { takeDamageOnBoss }");
                            break;

                        case "Сдаться":
                            heroHealth = 0;
                            break;

                        default:
                            Console.WriteLine("Вы пропустили ваш ход!");                            
                            break;
                    }
                    takeDamageOnHero = bossBaseDamage*Convert.ToDouble(luckyNumberForHit.Next(startIndexForRandom, endIndexForRandom) > magickShield);
                    if (luckyNumberForCrit.Next(startIndexForRandom, endIndexForRandom) <= bossCritChance) 
                    {
                        takeDamageOnHero *= critDamage;
                    }                    
                    heroHealth -= takeDamageOnHero;

                    Console.WriteLine($"Атака босса нанесла: {takeDamageOnHero} урона \nНажмите Enter для продолжения..");
                    Console.ReadLine();
                    Console.Clear();

                    if(bossHealth <= 0 || heroHealth <= 0)
                    {
                        startGame = false;
                    }
                }
            }
            else
            {
                Console.WriteLine("Трус!");
            }

            if (heroHealth <= 0 && bossHealth <= 0)
            {
                Console.WriteLine("Эта битва была легендарной! Но никто не победил..");
            }
            else if (heroHealth <= 0)
            {
                Console.WriteLine("Вы проиграли! Босс оказался сильнее..");
            }
            else if (bossHealth <= 0)
            {
                Console.WriteLine("Мои поздровления, вы выиграли!");
            }











        }
    }
}
