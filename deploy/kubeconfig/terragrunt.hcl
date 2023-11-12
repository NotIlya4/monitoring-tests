dependency "kind" {
  config_path = "../kind"
}

include "root" {
  path = find_in_parent_folders()
}

inputs = {
  kubeconfig_path = dependency.kind.outputs.kubeconfig_path
  context = dependency.kind.outputs.context
}