<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:template match="/">
		<PatientInfo>
			<guid><xsl:value-of select="PatientInfo/guid"/></guid>
			<situation><xsl:value-of select="PatientInfo/situation"/></situation>
			
	      	<xsl:choose>
	          <xsl:when test="PatientInfo/sourceType = 1">
	          	<sourceType id="1">Paramédico</sourceType>
	          </xsl:when>
	          <xsl:when test="PatientInfo/sourceType = 2">
	          	<sourceType id="2">Operador CCC</sourceType>
	          </xsl:when>
	          <xsl:when test="PatientInfo/sourceType = 3">
	          	<sourceType id="3">CA</sourceType>
	          </xsl:when>
	        </xsl:choose>
			
			<sourceId><xsl:value-of select="PatientInfo/sourceID"/></situation>
			<xsl:if test="PatientInfo/name"><name><xsl:value-of select="PatientInfo/name"/></name></xls:if>
			<xsl:if test="PatientInfo/bi"><bi><xsl:value-of select="PatientInfo/bi"/></bi></xls:if>
			<xsl:if test="PatientInfo/pressureMin"><pressureMin><xsl:value-of select="PatientInfo/pressureMin"/></pressureMin></xls:if>
			<xsl:if test="PatientInfo/pressureMax"><pressureMax><xsl:value-of select="PatientInfo/pressureMax"/></pressureMax></xls:if>
			<xsl:if test="PatientInfo/pulsation"><pulsation><xsl:value-of select="PatientInfo/pulsation"/></pulsation></xls:if>
			<xsl:if test="PatientInfo/diabets"><diabets><xsl:value-of select="PatientInfo/diabets"/></diabets></xls:if>
			<xsl:if test="PatientInfo/bloodType"><bloodType><xsl:value-of select="PatientInfo/bloodType"/></bloodType></xls:if>
			<gravity><xsl:value-of select="PatientInfo/gravity"/></gravity>
			<xsl:if test="PatientInfo/hospital"><hospital><xsl:value-of select="PatientInfo/hospital"/></hospital></xls:if>
			<xsl:if test="PatientInfo/comments"><comments><xsl:value-of select="PatientInfo/comments"/></comments></xls:if>
			<isDead><xsl:value-of select="PatientInfo/isDead"/></isDead>
			<xsl:if test="PatientInfo/zones">
				<spots>
					<xsl:for-each select="PatientInfo/zones">
				      	<xsl:choose>
				          <xsl:when test="substring-before(zone,'=')=12">
				          	<heart>
				            	<description><xsl:value-of select="substring-after(zone,'=')"/></description>
				            </heart>
				          </xsl:when>
				          <xsl:when test="substring-before(zone,'=')=13">
				          	<head>
				            	<description><xsl:value-of select="substring-after(zone,'=')"/></description>
				            </head>
				          </xsl:when>
				          <xsl:otherwise>
				            <unknown>Not supported</unknown>
				          </xsl:otherwise>
				        </xsl:choose>
					</xsl:for-each>
				</spots>
			</xls:if>
		</PatientInfo>
	</xsl:template>

</xsl:stylesheet>