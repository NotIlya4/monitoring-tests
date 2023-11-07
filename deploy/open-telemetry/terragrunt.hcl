include "root" {
  path = find_in_parent_folders()
}

include "kubernetes" {
  path = "../kubernetes-provider.hcl"
}
