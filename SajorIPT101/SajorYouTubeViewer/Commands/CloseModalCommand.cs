using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajorIPT101Solution.SajorYouTubeViewer.Stores;

namespace SajorIPT101Solution.SajorYouTubeViewer.Commands
{
    public class CloseModalCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public CloseModalCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            _modalNavigationStore.Close();
        }
    }
}
