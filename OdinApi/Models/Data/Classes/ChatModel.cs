using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using OdinApi.Controllers;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OdinApi.Models.Data.Classes
{
    public class ChatModel : IChatModel
    {

        private readonly OdinContext _context;

        public ChatModel(OdinContext context)
        {
            _context = context;
        }
        public List<Chat> GetChat()
        {
            try
            {
                var query = _context.Chat.ToList();

                if (query != null)
                {
                    return query;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return new List<Chat>();
            }
        }

        public Chat PostChat(Chat chat)
        {  
            _context.Chat.Add(chat);
            _context.SaveChanges();
            return chat;
        }

        public Chat GetChatById(int id)
        {
            try
            {
                var query = _context.Chat.Find(id);

                if (query.Id != 0)
                {
                    return query;
                }
                else
                {
                    return new Chat();
                }
            }
            catch (Exception)
            {
                return new Chat();
            }
        }

        public Chat PutChat(Chat chat)
        {
            try
            {
                _context.Update(chat);
                _context.SaveChanges();
                return chat;
            }
            catch (Exception)
            {
                return new Chat();
            }
        }

        public bool DeleteChat(int id)
        {
            try
            {
                Chat chat = _context.Chat.Find(id);
                if (chat != null)
                {
                    _context.Remove(chat);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
