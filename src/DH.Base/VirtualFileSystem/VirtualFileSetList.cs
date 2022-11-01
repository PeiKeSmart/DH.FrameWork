﻿namespace DH.VirtualFileSystem
{
    public class VirtualFileSetList : List<IVirtualFileSet>
    {
        public List<string> PhysicalPaths { get; }

        public VirtualFileSetList()
        {
            PhysicalPaths = new List<string>();
        }
    }
}
