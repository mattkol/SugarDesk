using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using SugarDesk.Restful.Models;

namespace SugarDesk.Restful.Messages
{
    public class AccountMessage : PubSubEvent<SugarCrmAccount>
    {
    }
}
