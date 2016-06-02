<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="VikramPOC" generation="1" functional="0" release="0" Id="18cc5231-8ac1-4de9-adf9-c083dfddef2a" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="VikramPOCGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="VikramWebRole:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/VikramPOC/VikramPOCGroup/LB:VikramWebRole:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="VikramWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/VikramPOC/VikramPOCGroup/MapVikramWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="VikramWebRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/VikramPOC/VikramPOCGroup/MapVikramWebRoleInstances" />
          </maps>
        </aCS>
        <aCS name="VikramWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/VikramPOC/VikramPOCGroup/MapVikramWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="VikramWorkerRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/VikramPOC/VikramPOCGroup/MapVikramWorkerRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:VikramWebRole:Endpoint1">
          <toPorts>
            <inPortMoniker name="/VikramPOC/VikramPOCGroup/VikramWebRole/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapVikramWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/VikramPOC/VikramPOCGroup/VikramWebRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapVikramWebRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/VikramPOC/VikramPOCGroup/VikramWebRoleInstances" />
          </setting>
        </map>
        <map name="MapVikramWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/VikramPOC/VikramPOCGroup/VikramWorkerRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapVikramWorkerRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/VikramPOC/VikramPOCGroup/VikramWorkerRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="VikramWebRole" generation="1" functional="0" release="0" software="C:\Users\vikram.singh\Documents\Visual Studio 2015\Projects\VikramPOC\VikramPOC\csx\Debug\roles\VikramWebRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;VikramWebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;VikramWebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;VikramWorkerRole&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/VikramPOC/VikramPOCGroup/VikramWebRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/VikramPOC/VikramPOCGroup/VikramWebRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/VikramPOC/VikramPOCGroup/VikramWebRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="VikramWorkerRole" generation="1" functional="0" release="0" software="C:\Users\vikram.singh\Documents\Visual Studio 2015\Projects\VikramPOC\VikramPOC\csx\Debug\roles\VikramWorkerRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;VikramWorkerRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;VikramWebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;VikramWorkerRole&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/VikramPOC/VikramPOCGroup/VikramWorkerRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/VikramPOC/VikramPOCGroup/VikramWorkerRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/VikramPOC/VikramPOCGroup/VikramWorkerRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="VikramWebRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="VikramWorkerRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="VikramWebRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="VikramWorkerRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="VikramWebRoleInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="VikramWorkerRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="a30f87a4-340b-4a1b-ac8b-8009426ce5fb" ref="Microsoft.RedDog.Contract\ServiceContract\VikramPOCContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="3a607ad0-b09e-42da-9284-2f2afa54a942" ref="Microsoft.RedDog.Contract\Interface\VikramWebRole:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/VikramPOC/VikramPOCGroup/VikramWebRole:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>