using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueOfLegendsDB
{
    public class CharacterRepository
    {
        private const int PageSize = 4;

        private readonly ApplicationContext _context;

        public CharacterRepository()
        {
            _context = new ApplicationContext();
        }

        // CRUD для персонажів
        // Create
        public void AddCharacter(Character character)
        {
            _context.Characters.Add(character);
            _context.SaveChanges();
        }

        public void AddCharacters(List<Character> characters)
        {
            _context.Characters.AddRange(characters);
            _context.SaveChanges();
        }   

        // Read
        public List<Character> GetAllCharacters()
        {
            return _context.Characters.Include(c => c.Class).ToList();
        }

        public Character GetCharacterById(int characterId)
        {
            return _context.Characters.Find(characterId);
        }

        // Update
        public void UpdateCharacter(Character updatedCharacter)
        {
            var existingCharacter = _context.Characters.Find(updatedCharacter.Id);
            if (existingCharacter != null)
            {
                existingCharacter.Name = updatedCharacter.Name;
                existingCharacter.Class = updatedCharacter.Class;
                existingCharacter.BlueEssence = updatedCharacter.BlueEssence;
                existingCharacter.RiotPoints = updatedCharacter.RiotPoints;
                existingCharacter.ImagePath = updatedCharacter.ImagePath;

                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Character not found");
            }
        }

        // Delete
        public void DeleteCharacter(int characterId)
        {
            var characterToDelete = _context.Characters.Find(characterId);
            if (characterToDelete != null)
            {
                _context.Characters.Remove(characterToDelete);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Character not found");
            }
        }

        // LINQ to Entities
        public List<Character> GetCharactersByName(string name)
        {
            return _context.Characters.Where(c => c.Name == name).Include(c => c.Class).ToList();
        }

        public List<Character> GetCharactersByBERange(int minBE, int maxBE)
        {
            return _context.Characters.Where(c => c.BlueEssence >= minBE && c.BlueEssence <= maxBE).Include(c => c.Class).ToList();
        }

        // Pagination
        public List<Character> GetCharactersPage(int pageNumber)
        {
            int skipAmount = (pageNumber - 1) * PageSize;

            return _context.Characters
                .OrderBy(c => c.Id)
                .Skip(skipAmount)
                .Include(c => c.Class)
                .Take(PageSize)
                .ToList();
        }

        // CRUD для Класів

        public List<Class> GetAllClasses()
        {
            return _context.Classes.ToList();
        }

        public Class GetClassById(int id)
        {
            return _context.Classes.Find(id);
        }

        public void AddClass(Class Class)
        {
            _context.Classes.Add(Class);
            _context.SaveChanges();
        }

        public void UpdateClass(Class Class)
        {
            _context.Entry(Class).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteClass(int id)
        {
            var Class = _context.Classes.Find(id);
            if (Class != null)
            {
                _context.Classes.Remove(Class);
                _context.SaveChanges();
            }
        }
    }
}
