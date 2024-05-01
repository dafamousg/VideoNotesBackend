using Nelibur.ObjectMapper;
using VideoNotesBackend.ModelDto;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.Helpers.Converter
{
    public class Converter
    {
        public static Td TypeToDto<Ts, Td>(Ts obj)
        {
            TinyMapper.Bind<Ts, Td>();
            return TinyMapper.Map<Td>(obj);
        }
    }
}
