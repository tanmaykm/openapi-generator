package org.openapitools.codegen.languages;

import org.openapitools.codegen.*;

import java.io.File;

public class JuliaServerCodegen extends AbstractJuliaCodegen {
    /**
     * Configures the type of generator.
     *
     * @return the CodegenType for this generator
     * @see org.openapitools.codegen.CodegenType
     */
    public CodegenType getTag() {
        return CodegenType.SERVER;
    }

    /**
     * Configures the name of the generator.
     * This will be used to refer to the generator when configuration is read from config files.
     *
     * @return the name of the generator
     */
    public String getName() {
        return "julia-server";
    }

    /**
     * Configures a help message for the generator.
     * TODO: add parameters, tips here
     *
     * @return the help message for the generator
     */
    public String getHelp() {
        return "Generates a julia server.";
    }

    public JuliaServerCodegen() {
        super();

        outputFolder = "generated-code" + File.separator + "julia-server";
        modelTemplateFiles.put("model.mustache", ".jl");
        apiTemplateFiles.put("api.mustache", ".jl");
        embeddedTemplateDir = templateDir = "julia-server";

        supportingFiles.clear();
        supportingFiles.add(new SupportingFile("README.mustache", "README.md"));
    }

    @Override
    public void processOpts() {
        super.processOpts();
        if (additionalProperties.containsKey(CodegenConstants.PACKAGE_NAME)) {
            setPackageName((String) additionalProperties.get(CodegenConstants.PACKAGE_NAME));
        }
        else {
            setPackageName("APIServer");
            additionalProperties.put(CodegenConstants.PACKAGE_NAME, packageName);
        }
        supportingFiles.add(new SupportingFile("server.mustache", srcPath, packageName + ".jl"));
        supportingFiles.add(new SupportingFile("modelincludes.mustache", srcPath, "modelincludes.jl"));
    }
}
