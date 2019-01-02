using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.TextTemplating;
using System.IO;

namespace EduardoPanero.CodeGeneration
{
    public class ProjectManager
    {

        private readonly DTE _dte;
        private readonly ITextTemplatingEngineHost _host;

        public ProjectManager(ITextTemplatingEngineHost host)
        {
            _host = host;
            IServiceProvider serviceProvider = (IServiceProvider)_host;
            _dte = serviceProvider.GetService(typeof(DTE)) as DTE;
        }


        public string AssemblyName
        {
            get
            {
                return GetProject().Properties.Item("OutputFileName").Value.ToString();
            }
        }

        public string AssemblyPath
        {
            get
            {
                return Path.Combine(ProjectPath, OutputPath, AssemblyName);
            }
        }

        public string ProjectName
        {
            get
            {
                return GetProject().Name;
            }
        }

        public string ProjectPath
        {
            get
            {
                return GetProject().Properties.Item("FullPath").Value.ToString(); ;
            }
        }

        public Project GetProject()
        {
            return _dte.Solution.FindProjectItem(_host.TemplateFile).ContainingProject;
        }

        public string OutputPath
        {
            get
            {
                return GetProject().ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString();
            }
        }

        public void SyncFile(string file)
        {
            GetProject().ProjectItems.AddFromFile(file);
        }

    }

}
