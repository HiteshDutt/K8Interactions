# K8 Interactions
## Introduction
This is an C# based API solution to create deployment [Pre-configured with image to deploy, limits and mounts etc], deployment then can be mapped to service and service can be added as a rule to ingress.

Current configuration of this service deploys an application that helps in vizualization of 3D raw images.

## Requirements
1. install Kubectl
2. Update the code deployment.cs file to fetch image from correct registry, add imagepullsecret if needed for pulling image from registry.
3. update the code deployment.cs to point correct nodepool, current code will try to deploy target application in threedpool.
4. update the resources needed to run target application, current application creates target app with 10CPU and 20Gi as request and 20CPU and 40Gi as limit


## Pre-Deployment
This application needs Admin rights on cluster, for same we will create a ServiceAccount (sa) with Admin ClusterRole

### Creating SA
Run the following script for creating a new Service Account with Name admin
```
kubectl apply -f - <<EOF
apiVersion: v1
kind: ServiceAccount
metadata:
  name: admin
EOF
```

### Creating ClusterRoleBinding
Run following script for creating new ClusterRoleBinding for admin storage account
```
kubectl apply -f - <<EOF
apiVersion: rbac.authorization.k8s.io/v1
kind: ClusterRoleBinding
metadata:
  name: adminclusterrolebinding
roleRef:
  apiGroup: rbac.authorization.k8s.io
  kind: ClusterRole
  name: admin
subjects:
- kind: ServiceAccount
  name: admin
  namespace: default
EOF
```

### Using Service Account in Deployment
Update deployment yaml to include newly created service account [this is not a complete pod defination]
Please check deployment.yaml for all details
```
apiVersion: v1
kind: Pod
metadata:
  name: nginx
spec:
  containers:
  - image: test
    name: test
  serviceAccountName: admin
```
to deploy k8 interaction application please apply following [please make required changes to deployment.yaml]
```
kubectl apply -f deployment.yaml
```
