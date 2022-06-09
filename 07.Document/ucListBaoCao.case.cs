		
                case "mnuBLThangTH":
                    {
                        ucDSNhanVienThayDoiLuongGD tmp = new ucDSNhanVienThayDoiLuongGD();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }
                
                
                
                default:
                    {
                        ucBlank tmp = new ucBlank();
                        panel2.Controls.Clear();
                        panel2.Controls.Add(tmp);
                        tmp.Dock = DockStyle.Fill;
                        break;
                    }